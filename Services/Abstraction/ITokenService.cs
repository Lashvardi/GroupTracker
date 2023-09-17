using GroupTracker.Models;

namespace GroupTracker.Services.Abstraction;

public interface ITokenService
{
    string CreateToken(Lecturer lecturer);
    bool ValidateToken(string token);
}
