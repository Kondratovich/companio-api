using Companio.Models;
using Companio.Mongo;
using Companio.Services.Interfaces;

namespace Companio.Services;

public class ProjectService : ServiceBase<Project>, IProjectService
{
    public ProjectService(MongoContext mongoContext) : base(mongoContext)
    {
    }

    public new Project Create(Project project)
    {
        project.DateAdded = DateTime.UtcNow;
        return base.Create(project);
    }
}