namespace Companio.DTO;

public class TaskDTO
{
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
}

public class TaskReadDTO : TaskDTO
{
    public string Id { get; set; }
}