using MongoDB.Bson;

namespace Companio.Models;

public class Team
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}