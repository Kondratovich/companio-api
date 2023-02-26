using Companio.DTO;
using Companio.Models;
using Companio.Models.Enums;

namespace Companio.Services.Interfaces;

public interface IAuthService
{
    User AuthenticateUser(UserDTO userDto);
    User RegisterUser(string email, string password, Role role);
    bool IsEmailAvailable(string email);
}