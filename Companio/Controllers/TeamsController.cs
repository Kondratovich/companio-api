using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

[ApiController]
[Route("api/v1/teams")]
public class TeamsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITeamService _teamService;

    public TeamsController(IMapper mapper, ITeamService teamService)
    {
        _mapper = mapper;
        _teamService = teamService;
    }

    [HttpGet]
    public ActionResult<List<TeamReadDTO>> GetAll()
    {
        var teams = _teamService.GetAll();
        var teamReadDtos = teams.Select(_mapper.Map<TeamReadDTO>);
        return Ok(teamReadDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<TeamReadDTO> Get(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(guidId);

        if (team == null)
            return NotFound();

        var teamReadDto = _mapper.Map<TeamReadDTO>(team);

        return Ok(teamReadDto);
    }

    [HttpPost]
    public ActionResult<TeamReadDTO> Create([FromBody] TeamDTO teamDto)
    {
        var team = _mapper.Map<Team>(teamDto);
        var createdTeam = _teamService.Create(team);
        var teamReadDto = _mapper.Map<TeamReadDTO>(createdTeam);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/teams/{team.Id}";

        return Created(locationUri, teamReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] TeamDTO teamDto)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(guidId);
        if (team == null)
            return NotFound();

        _mapper.Map(teamDto, team);
        _teamService.Update(team);
        var outputDto = _mapper.Map<TeamReadDTO>(team);

        return Ok(outputDto);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(guidId);
        if (team == null)
            return NotFound();

        _teamService.Delete(guidId);

        return NoContent();
    }
}