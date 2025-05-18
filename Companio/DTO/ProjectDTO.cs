using Companio.Validation;

namespace Companio.DTO;

public class ProjectDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    [ObjectId] public string CustomerId { get; set; }
    [ObjectId] public string TeamId { get; set; }
    public decimal Price { get; set; }
    public DateTime Deadline { get; set; }
}

public class ProjectReadDTO : ProjectDTO
{
    public string Id { get; set; }
    public DateTime DateAdded { get; set; }
}