using MongoDB.Bson;

namespace Companio.Models;

public class Project
{
    public ObjectId Id { get; set; }
    public ObjectId CustomerId { get; set; }
    public ObjectId TeamId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime Deadline { get; set; }
    public decimal Price { get; set; }
}