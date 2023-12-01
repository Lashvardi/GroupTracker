using GroupTracker.data;
using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction;
using GroupTracker.Services.Abstraction.ImageUpload;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMTP.Authorization.EmailVerification;

namespace GroupTracker.Services.Implementation;

public class LecturerService : ILecturerService
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<LecturerRegistrationInput> _hasher;
    private readonly EmailVerification _emailVerification;
    private readonly IImageUploadService _imageUploadService;

    public LecturerService(DataContext context, ITokenService tokenService, PasswordHasher<LecturerRegistrationInput> hasher, EmailVerification emailVerification, IImageUploadService imageUploadService)
    {
        _context = context;
        _tokenService = tokenService;
        _hasher = hasher;
        _emailVerification = emailVerification;
        _imageUploadService = imageUploadService;
    }

    public async Task<LecturerRegistrationResponse> RegisterLecturer(LecturerRegistrationInput input)
    {
        if (await _context.Lecturers.AnyAsync(x => x.Email == input.Email))
        {
            throw new Exception("Lecturer Is Already Registered");
        }

        var lecturer = new Lecturer
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            Subjects = input.Subjects,
            Password = _hasher.HashPassword(input, input.Password),
            Companies = input.Companies
        };

        await _context.Lecturers.AddAsync(lecturer);
        await _context.SaveChangesAsync();

        await SendVerificationEmail(input.Email);

        return new LecturerRegistrationResponse
        {
            Email = lecturer.Email,
            FullName = $"{lecturer.FirstName} {lecturer.LastName}"
        };
    }

    public async Task<string> SendVerificationEmail(string email)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Email == email);

        if (lecturer == null)
        {
            throw new Exception("Lecturer Is Not Registered");
        }

        if (lecturer.IsVerified)
        {
            throw new Exception("Lecturer Is Already Verified");
        }

        var verificationCode = new Random().Next(100000, 999999).ToString();

        await _emailVerification.VerifyEmail(email, verificationCode);

        lecturer.VerificationCode = verificationCode;
        await _context.SaveChangesAsync();

        return verificationCode;
    }

    public async Task<string> VerifyLecturer(string email, string token)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Email == email);

        if (lecturer == null)
        {
            throw new Exception("Lecturer Is Not Registered");
        }

        if (lecturer.IsVerified)
        {
            throw new Exception("Lecturer Is Already Verified");
        }

        if (lecturer.VerificationCode != token)
        {
            throw new Exception("Invalid Verification Code");
        }

        lecturer.IsVerified = true;
        lecturer.VerificationCode = "VERIFIED";

        await _context.SaveChangesAsync();
        return lecturer.VerificationCode;
    }

    public async Task<string> LoginLecturer(string email, string password)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Email == email);

        if (lecturer == null)
        {
            throw new Exception("Invalid email or password");
        }

        if(lecturer.IsVerified == false)
        {
            throw new Exception("Lecturer is not verified, Please Check Your Email");
        }

        var result = _hasher.VerifyHashedPassword(null, lecturer.Password, password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid email or password");
        }

        return _tokenService.CreateToken(lecturer);
    }

    public async Task<LecturerDTO> GetLecturerInfoAsync(int lecturerId)
    {
        var lecturer = await _context.Lecturers
               .Include(l => l.LecturerGroups)
                   .ThenInclude(lg => lg.GroupLectureSessions)
                       .ThenInclude(gls => gls.LectureSession)
               .Include(l => l.LecturerGroups)
                   .ThenInclude(lg => lg.CurrentSyllabusTopic)
               .FirstOrDefaultAsync(l => l.Id == lecturerId);


        if (lecturer == null) return null;


        var Fullname = $"{lecturer.FirstName} {lecturer.LastName}";
        var lecturerDto = new LecturerDTO
        {
            Email = lecturer.Email,
            FullName = Fullname,
            Companies = new List<string>(lecturer.Companies.Split(',').Select(c => c.Trim())),
            Subjects = new List<string>(lecturer.Subjects.Split(',').Select(c => c.Trim())),
            GroupCount = lecturer.LecturerGroups.Count,
            Groups = lecturer.LecturerGroups.Select(lg => new LecturerGroupDTO
            {
                CompanyName = lg.CompanyName,
                GroupName = lg.GroupName,
                Grade = lg.Grade,
                TopicName = lg.CurrentSyllabusTopic?.Title,
                TopicId = lg.CurrentSyllabusTopic?.Id,
                Status = lg.Status,
                HEXColor = lg.HEX,
                GroupType = lg.GroupType,
                SessionsAmount = lg.SessionsAmount,

                Sessions = lg.GroupLectureSessions.Select(gls => new GroupLectureSessionDTO
                {
                    SessionDate = gls.LectureSession.Day,
                    SessionTime = gls.LectureSession.Time.ToString(),
                    IsOnline = gls.LectureSession.IsOnline,
                    Auditorium = gls.LectureSession.Auditorium,
                }).ToList()
            }).ToList(),
            groupNameWithIds = lecturer.LecturerGroups.Select(lg => new GroupNameWithId
            {
                GroupName = lg.GroupName,
                GroupId = lg.Id,
            }).ToList()
        };

        return lecturerDto;
    }

    public async Task<List<LecturerSubjects>> GetLecturerSubjects(int id)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == id);

        if (lecturer == null)
        {
            throw new Exception("Lecturer Is Not Registered");
        }

        var subjects = lecturer.Subjects.Split(",").ToList();

        var lecturerSubjects = subjects.Select(subject => new LecturerSubjects
        {
            SubjectName = subject
        }).ToList();

        return lecturerSubjects;
    }

    public async Task<List<LecturerCompanies>> GetLecturerCompanies(int id)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == id);

        if (lecturer == null)
        {
            throw new Exception("Lecturer Is Not Registered");
        }

        var companies = lecturer.Companies.Split(",").ToList();

        var lecturerCompanies = companies.Select(company => new LecturerCompanies
        {
            CompanyName = company
        }).ToList();

        return lecturerCompanies;
    }

    public async Task<IFormFile> UploadProfilePictureAsync(int lecturerId, IFormFile file)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        if (file == null)
        {
            throw new Exception("File is null");
        }
        using var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());

        var previousImageUrl = lecturer.ImageUrl;

        if (previousImageUrl != null)
        {
            _imageUploadService.DeleteImage(previousImageUrl);
        }

        int maxWidth = 400;
        int maxHeight = 400;
        int originalWidth = image.Width;
        int originalHeight = image.Height;
        double ratioX = (double)maxWidth / originalWidth;
        double ratioY = (double)maxHeight / originalHeight;
        double ratio = Math.Min(ratioX, ratioY);

        int newWidth = (int)(originalWidth * ratio);
        int newHeight = (int)(originalHeight * ratio);

        image.Mutate(x => x.Resize(newWidth, newHeight));

        var tempFileName = Path.GetTempFileName();
        // Save the resized image as a new file
        await using (var tempFileStream = new FileStream(tempFileName, FileMode.Create))
        {
            await image.SaveAsJpegAsync(tempFileStream);
        }

        // Create a new FormFile for the resized image
        await using var resizedFileStream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
        var resizedFile = new FormFile(resizedFileStream, 0, resizedFileStream.Length, file.Name, file.FileName);

        // Save the resized image using your existing service
        var fileResult = _imageUploadService.SaveImage(resizedFile);

        if (fileResult.Item1 == 1)
        {
            lecturer.ImageUrl = fileResult.Item2;
        }

        _context.Update(lecturer);
        await _context.SaveChangesAsync();

        // Properly dispose of the file stream before attempting to delete the file
        await resizedFileStream.DisposeAsync();

        File.Delete(tempFileName);

        // Return the original file (or you might want to return resizedFile if that's intended)
        return file;
    }



    public Task<string> GetProfilePictureAsync(int lecturerId)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        return Task.FromResult(lecturer.ImageUrl);

    }

    public async Task<IFormFile> UploadBannerPictureAsync(int lecturerId, IFormFile file)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        if (file == null)
        {
            throw new Exception("File is null");
        }
        using var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());

        var previousImageUrl = lecturer.bannerImageUrl;

        if (previousImageUrl != null)
        {
            _imageUploadService.DeleteImage(previousImageUrl);
        }

        int maxWidth = 1920;
        int maxHeight = 1200;
        int originalWidth = image.Width;
        int originalHeight = image.Height;
        double ratioX = (double)maxWidth / originalWidth;
        double ratioY = (double)maxHeight / originalHeight;
        double ratio = Math.Min(ratioX, ratioY);

        int newWidth = (int)(originalWidth * ratio);
        int newHeight = (int)(originalHeight * ratio);

        image.Mutate(x => x.Resize(newWidth, newHeight));

        var tempFileName = Path.GetTempFileName();
        // Save the resized image as a new file
        await using (var tempFileStream = new FileStream(tempFileName, FileMode.Create))
        {
            await image.SaveAsJpegAsync(tempFileStream);
        }

        // Create a new FormFile for the resized image
        await using var resizedFileStream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
        var resizedFile = new FormFile(resizedFileStream, 0, resizedFileStream.Length, file.Name, file.FileName);

        // Save the resized image using your existing service
        var fileResult = _imageUploadService.SaveImage(resizedFile);

        if (fileResult.Item1 == 1)
        {
            lecturer.bannerImageUrl = fileResult.Item2;
        }

        _context.Update(lecturer);
        await _context.SaveChangesAsync();

        // Properly dispose of the file stream before attempting to delete the file
        await resizedFileStream.DisposeAsync();

        File.Delete(tempFileName);

        // Return the original file (or you might want to return resizedFile if that's intended)
        return file;
    }

    public Task<string> GetbannerImageAsync(int lecturerId)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        return Task.FromResult(lecturer.bannerImageUrl);
    }

    public void DeleteProfilePicture(int lecturerId)

    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        var previousImageUrl = lecturer.ImageUrl;
        lecturer.ImageUrl = null;

        if (previousImageUrl != null)
        {
            _imageUploadService.DeleteImage(previousImageUrl);
        }
        else
        {
            lecturer.ImageUrl = null;
            throw new Exception("Lecturer Does Not Have A Profile Picture");
        }


        _context.Update(lecturer);
        _context.SaveChanges();
    }

    public void DeleteBannerPicture(int lecturerId)
    {
    var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        var previousImageUrl = lecturer.bannerImageUrl;
        lecturer.bannerImageUrl = null;

        if (previousImageUrl != null)
    {
            _imageUploadService.DeleteImage(previousImageUrl);
        }
        else
        {
            lecturer.bannerImageUrl = null;
            throw new Exception("Lecturer Does Not Have A Banner Picture");
        }


        _context.Update(lecturer);
        _context.SaveChanges();
    }

    public Task<LecturerSocialsDTO> AddSocialsAsync(int lecturerId, LecturerSocialsDTO socials)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        lecturer.FacebookLink = socials.FacebookLink ?? lecturer.FacebookLink;
        lecturer.TwitterLink = socials.TwitterLink ?? lecturer.TwitterLink;
        lecturer.InstagramLink = socials.InstagramLink ?? socials.InstagramLink;
        lecturer.LinkedInLink = socials.LinkedInLink ?? lecturer.LinkedInLink;
        lecturer.YouTubeLink = socials.YouTubeLink ?? lecturer.YouTubeLink;
        lecturer.PersonalWebsiteLink = socials.PersonalWebsiteLink ?? socials.PersonalWebsiteLink;

        _context.Update(lecturer);
        _context.SaveChanges();

        return Task.FromResult(socials);
    }

    public Task<LecturerSocialsDTO> GetSocialsAsync(int lecturerId)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        var socials = new LecturerSocialsDTO
        {
            FacebookLink = lecturer.FacebookLink,
            TwitterLink = lecturer.TwitterLink,
            InstagramLink = lecturer.InstagramLink,
            LinkedInLink = lecturer.LinkedInLink,
            YouTubeLink = lecturer.YouTubeLink,
            PersonalWebsiteLink = lecturer.PersonalWebsiteLink
        };


        return Task.FromResult(socials);
    }

    public Task<bool> HasFilledOutSocialsAsync(int lecturerId)
    {
        var lecturer = _context.Lecturers.FirstOrDefault(x => x.Id == lecturerId) ?? throw new Exception("Lecturer Is Not Registered");

        if (lecturer.FacebookLink.IsNullOrEmpty() && lecturer.TwitterLink.IsNullOrEmpty() && lecturer.InstagramLink.IsNullOrEmpty() && lecturer.LinkedInLink.IsNullOrEmpty() && lecturer.YouTubeLink.IsNullOrEmpty() && lecturer.PersonalWebsiteLink.IsNullOrEmpty())
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public async Task DeleteLecturerAsync(int lecturerId)
    {
        var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == lecturerId);

        if (lecturer == null)
        {
            throw new Exception("Lecturer Is Not Registered");
        }

        _context.Lecturers.Remove(lecturer);
        await _context.SaveChangesAsync();
    }
}