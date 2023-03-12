using Companio.DTO;
using Companio.Models;
using Companio.Models.Enums;

namespace Companio.Services.Interfaces;

public interface IAuthService
{
    User AuthenticateUser(UserDTO userDto);
    User RegisterUser(UserDTO userDto);
    bool IsEmailAvailable(string email);
}