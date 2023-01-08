using Companio.DTO;
using Companio.Models;
using Companio.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Companio.Controllers;

public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("api/v1/projects/{projectId}")]
    public IActionResult Get([FromRoute] ObjectId projectId)
    {
        var project = _projectService.GetProjectById(projectId);

        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpGet("api/v1/projects")]
    public IActionResult GetAll()
    {
        var projects = _projectService.GetProjects();

        return Ok(projects);
    }

    [HttpPost("api/v1/projects")]
    public IActionResult Create([FromBody]ProjectDTO projectDto)
    {
        var project = new Project
        {
            Name = projectDto.Name,
            Id = string.IsNullOrWhiteSpace(projectDto.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(projectDto.Id)
        };

        //_projects.Add(project);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/projects/{project.Id}"; 
        
        return Created(locationUri, project);
    }
}