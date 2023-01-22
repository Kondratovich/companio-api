namespace Companio.DTO;

public class TeamDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class TeamReadDTO : TeamDTO
{
    public string Id { get; set; }
}