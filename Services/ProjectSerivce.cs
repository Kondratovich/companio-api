using Companio.Models;
using Companio.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Companio.Services;

public class ProjectService : IProjectService
{
    private readonly IMongoCollection<Project> _projects;

    public ProjectService(MongoContext context)
    {
        _projects = context.Provider.GetCollection<Project>("Project");
    }

    public List<Project> GetAll()
    {
        return _projects.Find(s => true).ToList();
    }

    public Project GetById(ObjectId id)
    {
        return _projects.Find(s => s.Id == id).FirstOrDefault();
    }

    public Project Create(Project project)
    {
        project.DateAdded = DateTime.UtcNow;
        _projects.InsertOne(project);
        return project;
    }

    public void Update(ObjectId id, Project project)
    {
        _projects.ReplaceOne(s => s.Id == id, project);
    }

    public void Delete(ObjectId id)
    {
        _projects.DeleteOne(s => s.Id == id);
    }
}