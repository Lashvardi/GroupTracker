using GroupTracker.data;
using GroupTracker.DTOs.Groups;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.Models;
using GroupTracker.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.Services.Implementation;

public class LecturerService : ILecturerService
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<LecturerRegistrationInput> _hasher;

    public LecturerService(DataContext context, ITokenService tokenService, PasswordHasher<LecturerRegistrationInput> hasher)
    {
        _context = context;
        _tokenService = tokenService;
        _hasher = hasher;
    }

    public async Task<LecturerRegistrationResponse> RegisterLecturer(LecturerRegistrationInput input)
    {
        var lecturer = new Lecturer
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Email = input.Email,
            Password = _hasher.HashPassword(input, input.Password),
            Companies = input.Companies
        };

        if (await _context.Lecturers.AnyAsync(x => x.Email == input.Email))
        {
            throw new Exception("Lecturer Is Already Registered");
        }

        await _context.Lecturers.AddAsync(lecturer);
        await _context.SaveChangesAsync();

        return new LecturerRegistrationResponse
        {
            Email = lecturer.Email,
            FullName = $"{lecturer.FirstName} {lecturer.LastName}"
        };
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
            Companies = string.Join(", ", lecturer.LecturerGroups.Select(lg => lg.CompanyName).Distinct()),
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
}
