using Companio.DTO;
using Companio.Models;

namespace Companio.Services.Interfaces;

public interface ITokenService
{
    string GenerateJWTToken(User user);
}