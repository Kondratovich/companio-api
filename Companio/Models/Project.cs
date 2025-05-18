namespace Companio.Models;

public class Project : DatabaseObject
{
    public Guid CustomerId { get; set; }
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime Deadline { get; set; }
    public decimal Price { get; set; }
}