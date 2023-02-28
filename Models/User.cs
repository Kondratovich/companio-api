using Companio.Models.Enums;
using MongoDB.Bson;

namespace Companio.Models;

public class User : DatabaseObject
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PasswordHash { get; set; }

    public Role Role { get; set; }

    public ObjectId TeamId { get; set; }

    public ObjectId ContractInfoId { get; set; }
}