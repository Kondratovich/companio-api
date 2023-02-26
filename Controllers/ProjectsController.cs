using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Companio.Controllers;

public class ProjectsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public ProjectsController(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    [HttpGet("api/v1/projects")]
    public ActionResult<List<ProjectReadDTO>> GetAll()
    {
        var projects = _projectService.GetAll();
        var projectReadDtos = projects.Select(_mapper.Map<ProjectReadDTO>);
        return Ok(projectReadDtos);
    }

    [HttpGet("api/v1/projects/{id}")]
    public ActionResult<ProjectReadDTO> Get(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(objectId);

        if (project == null)
            return NotFound();

        var projectReadDto = _mapper.Map<ProjectReadDTO>(project);

        return Ok(projectReadDto);
    }

    [HttpPost("api/v1/projects")]
    public ActionResult<ProjectReadDTO> Create(ProjectDTO projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        var createdProject = _projectService.Create(project);
        var projectReadDto = _mapper.Map<ProjectReadDTO>(createdProject);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/projects/{project.Id}"; 

        return Created(locationUri, projectReadDto);
    }

    [HttpPut("api/v1/projects/{id}")]
    public ActionResult Put(string id, ProjectDTO projectDto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(objectId);
        if (project == null)
            return NotFound();
 
        _mapper.Map(projectDto, project);
        _projectService.Update(project);
        var outputDto = _mapper.Map<ProjectReadDTO>(project);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/projects/{id}")]
    public ActionResult Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(objectId);
        if (project == null)
            return NotFound();

        _projectService.Delete(objectId);

        return NoContent();
    }
}