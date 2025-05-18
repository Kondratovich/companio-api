using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Companio.Controllers;

public class TeamsController : Controller
{
    private readonly IMapper _mapper;
    private readonly ITeamService _teamService;

    public TeamsController(IMapper mapper, ITeamService teamService)
    {
        _mapper = mapper;
        _teamService = teamService;
    }

    [HttpGet("api/v1/teams")]
    public ActionResult<List<TeamReadDTO>> GetAll()
    {
        var teams = _teamService.GetAll();
        var teamReadDtos = teams.Select(_mapper.Map<TeamReadDTO>);
        return Ok(teamReadDtos);
    }

    [HttpGet("api/v1/teams/{id}")]
    public ActionResult<TeamReadDTO> Get(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(objectId);

        if (team == null)
            return NotFound();

        var teamReadDto = _mapper.Map<TeamReadDTO>(team);

        return Ok(teamReadDto);
    }

    [HttpPost("api/v1/teams")]
    public ActionResult<TeamReadDTO> Create([FromBody] TeamDTO teamDto)
    {
        var team = _mapper.Map<Team>(teamDto);
        var createdTeam = _teamService.Create(team);
        var teamReadDto = _mapper.Map<TeamReadDTO>(createdTeam);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/teams/{team.Id}";

        return Created(locationUri, teamReadDto);
    }

    [HttpPut("api/v1/teams/{id}")]
    public ActionResult Put(string id, [FromBody] TeamDTO teamDto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(objectId);
        if (team == null)
            return NotFound();

        _mapper.Map(teamDto, team);
        _teamService.Update(team);
        var outputDto = _mapper.Map<TeamReadDTO>(team);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/teams/{id}")]
    public ActionResult Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var team = _teamService.SingleByIdOrDefault(objectId);
        if (team == null)
            return NotFound();

        _teamService.Delete(objectId);

        return NoContent();
    }
}