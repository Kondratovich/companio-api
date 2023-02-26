using Companio.DTO;
using Companio.Models;
using Companio.Models.Enums;
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
        var user = _context.GetAll<User>().SingleOrDefault(u => u.EmailAddress == userDto.Email && u.PasswordHash == userDto.Password);

        return user;
    }

    public User RegisterUser(string email, string password, Role role)
    {
        var user = new User
        {
            Id = ObjectId.GenerateNewId(),
            EmailAddress = email,
            PasswordHash = password,
            Role = role
        };

        _context.Create(user);

        return user;
    }

    public bool IsEmailAvailable(string email)
    {
        return _context.GetAll<User>().All(u => u.EmailAddress != email);
    }
}