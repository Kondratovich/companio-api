using Companio.DTO;
using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;
using MongoDB.Bson;

namespace Companio.Services;

public class AuthService : IAuthService
{
    private readonly MongoContext _context;

    public AuthService(MongoContext context)
    {
        _context = context;
    }

    public User AuthenticateUser(UserDTO userDto)
    {
        var user = _context.GetAll<User>().SingleOrDefault(u => u.Email == userDto.Email && u.PasswordHash == userDto.Password);

        return user;
    }

    public User RegisterUser(UserDTO userDto)
    {
        var user = new User
        {
            Id = ObjectId.GenerateNewId(),
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            TeamId = ObjectId.Parse(userDto.TeamId),
            PasswordHash = userDto.Password,
            Role = userDto.Role
        };

        _context.Create(user);

        return user;
    }

    public bool IsEmailAvailable(string email)
    {
        return _context.GetAll<User>().All(u => u.Email != email);
    }
}