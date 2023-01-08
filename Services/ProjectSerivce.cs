using Companio.Models;
using MongoDB.Bson;

namespace Companio.Services;

public class ProjectService : IProjectService
{
    private readonly List<Project> _projects;

    public ProjectService()
    {
        _projects = new List<Project>();
        for (int i = 0; i < 5; i++)
        {
            _projects.Add(new Project() { Id = ObjectId.GenerateNewId(), Name = $"ProjectName {i}" });
        }
    }

    public List<Project> GetProjects()
    {
        return _projects;
    }

    public Project GetProjectById(ObjectId projectId)
    {
        return _projects.SingleOrDefault(n => n.Id == projectId);
    }
}