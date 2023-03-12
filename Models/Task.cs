using MongoDB.Bson;

namespace Companio.Models;

public class Task : DatabaseObject
{
    public ObjectId ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
}