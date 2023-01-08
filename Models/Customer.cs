using MongoDB.Bson;

namespace Companio.Models;

public class Customer
{
    public ObjectId Id { get; set; }

    public string EmailAddress { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}