using GroupTracker.data;
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

        var result = _hasher.VerifyHashedPassword(null, lecturer.Password, password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Invalid email or password");
        }

        return _tokenService.CreateToken(lecturer);
    }
}
