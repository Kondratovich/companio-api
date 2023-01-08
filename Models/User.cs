using Companio.Models.Enums;
using Companio.Security;
using MongoDB.Bson;

namespace Companio.Models;

public class User
{
    public ObjectId Id { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PasswordHash { get; private set; }

    public Role Role { get; set; }

    public ObjectId TeamId { get; set; }
    
    public ObjectId ContractInfoId { get; set; }

    /// <summary>
    /// Date when we create the user. 
    /// </summary>
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Gets or sets the last date and time the user logged in to the system
    /// </summary>
    public DateTime LastLogin { get; set; }

    public void SetPassword(string password)
    {
        PasswordHash = PasswordHasher.Default.CreateHash(password);
    }
    public bool IsPasswordValid(string password) => PasswordHasher.Default.Verify(password, PasswordHash);
}