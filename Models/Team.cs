using MongoDB.Bson;

namespace Companio.Models;

public class Team : DatabaseObject
{
    public string Name { get; set; }
    public string Description { get; set; }
}