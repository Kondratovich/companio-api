using Companio.Models.Enums;

namespace Companio.Models;

public class User : DatabaseObject
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PasswordHash { get; set; }

    public Role Role { get; set; }

    public Guid TeamId { get; set; }
}