using Companio.Models.Enums;

namespace Companio.DTO;

public class UserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}