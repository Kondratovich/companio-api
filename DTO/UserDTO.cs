using Companio.Models.Enums;

namespace Companio.DTO;

public class UserDTO
{
    public string TeamId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}

public class UserReadDTO : UserDTO
{
    public string Id { get; set; }
}