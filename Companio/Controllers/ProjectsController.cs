using AutoMapper;
using Companio.Attributes;
using Companio.DTO;
using Companio.Models;
using Companio.Models.Enums;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

[ApiController]
[Route("api/v1/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public ProjectsController(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    [HttpGet]
    public ActionResult<List<ProjectReadDTO>> GetAll()
    {
        var projects = _projectService.GetAll();
        var projectReadDtos = projects.Select(_mapper.Map<ProjectReadDTO>);
        return Ok(projectReadDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<ProjectReadDTO> Get(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(guidId);

        if (project == null)
            return NotFound();

        var projectReadDto = _mapper.Map<ProjectReadDTO>(project);

        return Ok(projectReadDto);
    }

    [HttpPost]
    [RolePermission(Role.Administrator, Role.Manager)]
    public ActionResult<ProjectReadDTO> Create([FromBody] ProjectDTO projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var project = _mapper.Map<Project>(projectDto);
        var createdProject = _projectService.Create(project);
        var projectReadDto = _mapper.Map<ProjectReadDTO>(createdProject);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/projects/{project.Id}";

        return Created(locationUri, projectReadDto);
    }

    [HttpPut("{id}")]
    [RolePermission(Role.Administrator, Role.Manager)]
    public ActionResult Put(string id, [FromBody] ProjectDTO projectDto)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(guidId);
        if (project == null)
            return NotFound();

        _mapper.Map(projectDto, project);
        _projectService.Update(project);
        var outputDto = _mapper.Map<ProjectReadDTO>(project);

        return Ok(outputDto);
    }

    [HttpDelete("{id}")]
    [RolePermission(Role.Administrator, Role.Manager)]
    public ActionResult Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var project = _projectService.SingleByIdOrDefault(guidId);
        if (project == null)
            return NotFound();

        _projectService.Delete(guidId);

        return NoContent();
    }
}