using MongoDB.Bson;

namespace Companio.Models;

public class Customer : DatabaseObject
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Organization { get; set; }
}