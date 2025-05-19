using Companio.Data;
using Companio.Models;
using Companio.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Companio.Services;

public class ProjectService : ServiceBase<Project>, IProjectService
{
    private readonly ITeamService _teamService;
    private readonly ICustomerService _customerService;

    public ProjectService(ITeamService teamService, ICustomerService customerService, AppDbContext dbContext) : base(dbContext)
    {
        _teamService = teamService;
        _customerService = customerService;
    }

    public new Project Create(Project project)
    {
        var team = _teamService.SingleByIdOrDefault(project.TeamId);
        if (team == null)
            throw new ValidationException($"Team with {project.TeamId} doesn't exist");
        
        var customer = _customerService.SingleByIdOrDefault(project.CustomerId);
        if (customer == null)
            throw new ValidationException($"Customer with {project.CustomerId} doesn't exist");

        project.DateAdded = DateTime.UtcNow;
        return base.Create(project);
    }
}