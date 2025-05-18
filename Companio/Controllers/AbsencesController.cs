using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Companio.Controllers;

public class AbsencesController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAbsenceTimelineService _absenceTimelineService;

    public AbsencesController(IMapper mapper, IAbsenceTimelineService absenceTimelineService)
    {
        _mapper = mapper;
        _absenceTimelineService = absenceTimelineService;
    }

    [HttpGet("api/v1/absences")]
    public ActionResult<List<AbsenceTimelineReadDTO>> GetAll(string userId)
    {
        var absences = ObjectId.TryParse(userId, out var objectUserId)
            ? _absenceTimelineService.Find(a => a.UserId == objectUserId)
            : _absenceTimelineService.GetAll();

        var absenceReadDtos = absences.Select(_mapper.Map<AbsenceTimelineReadDTO>);
        return Ok(absenceReadDtos);
    }

    [HttpGet("api/v1/absences/{id}")]
    public ActionResult<AbsenceTimelineReadDTO> Get(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(objectId);

        if (absence == null)
            return NotFound();

        var absenceReadDto = _mapper.Map<AbsenceTimelineReadDTO>(absence);

        return Ok(absenceReadDto);
    }

    [HttpPost("api/v1/absences")]
    public ActionResult<AbsenceTimelineReadDTO> Create([FromBody] AbsenceTimelineDTO absenceDto)
    {
        var absence = _mapper.Map<AbsenceTimeline>(absenceDto);
        var createdAbsence = _absenceTimelineService.Create(absence);
        var absenceReadDto = _mapper.Map<AbsenceTimelineReadDTO>(createdAbsence);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/absences/{absence.Id}";

        return Created(locationUri, absenceReadDto);
    }

    [HttpPut("api/v1/absences/{id}")]
    public ActionResult Put(string id, [FromBody] AbsenceTimelineDTO absenceDto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(objectId);
        if (absence == null)
            return NotFound();

        _mapper.Map(absenceDto, absence);
        _absenceTimelineService.Update(absence);
        var outputDto = _mapper.Map<AbsenceTimelineReadDTO>(absence);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/absences/{id}")]
    public ActionResult Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(objectId);
        if (absence == null)
            return NotFound();

        _absenceTimelineService.Delete(objectId);

        return NoContent();
    }
}