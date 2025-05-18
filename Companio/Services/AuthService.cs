using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class AuthService : ServiceBase<User>, IAuthService
{
    public AuthService() : base()
    {
    }

    public User AuthenticateUser(UserDTO userDto)
    {
        var user = GetAll().SingleOrDefault(u => u.Email == userDto.Email && u.PasswordHash == userDto.Password);

        return user;
    }

    public User RegisterUser(UserDTO userDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            TeamId = Guid.Parse(userDto.TeamId),
            PasswordHash = userDto.Password,
            Role = userDto.Role
        };

        Create(user);

        return user;
    }

    public bool IsEmailAvailable(string email)
    {
        return GetAll().All(u => u.Email != email);
    }
}