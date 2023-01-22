namespace Companio.DTO;

public class ProjectDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class ProjectReadDTO : ProjectDTO
{
    public string Id { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime Deadline { get; set; }
    public decimal Price { get; set; }
}