using GroupTracker.data;
using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMTP.Authorization.EmailVerification;

namespace GroupTracker.Services.Implementation;

public class LecturerService : ILecturerService
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<LecturerRegistrationInput> _hasher;
    private readonly EmailVerification _emailVerification;

    public LecturerService(DataContext context, ITokenService tokenService, PasswordHasher<LecturerRegistrationInput> hasher, EmailVerification emailVerification)
    {
        _context = context;
        _tokenService = tokenService;
        _hasher = hasher;
        _emailVerification = emailVerification;
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
                Sessions = lg.GroupLectureSessions.Select(gls => new GroupLectureSessionDTO
                {
                    SessionDate = gls.LectureSession.Day,
                    SessionTime = gls.LectureSession.Time.ToString(),
                    IsOnline = gls.LectureSession.IsOnline,
                    Auditorium = gls.LectureSession.Auditorium,
                    IsAlternate = gls.LectureSession.IsAlternate,
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
}
