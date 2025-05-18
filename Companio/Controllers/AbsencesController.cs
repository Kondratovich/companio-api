using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

[ApiController]
[Route("api/v1/absences")]
public class AbsencesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAbsenceTimelineService _absenceTimelineService;

    public AbsencesController(IMapper mapper, IAbsenceTimelineService absenceTimelineService)
    {
        _mapper = mapper;
        _absenceTimelineService = absenceTimelineService;
    }

    [HttpGet]
    public ActionResult<List<AbsenceTimelineReadDTO>> GetAll(string userId)
    {
        var absences = Guid.TryParse(userId, out var guidUserId)
            ? _absenceTimelineService.Find(a => a.UserId == guidUserId)
            : _absenceTimelineService.GetAll();

        var absenceReadDtos = absences.Select(_mapper.Map<AbsenceTimelineReadDTO>);
        return Ok(absenceReadDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<AbsenceTimelineReadDTO> Get(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(guidId);

        if (absence == null)
            return NotFound();

        var absenceReadDto = _mapper.Map<AbsenceTimelineReadDTO>(absence);

        return Ok(absenceReadDto);
    }

    [HttpPost]
    public ActionResult<AbsenceTimelineReadDTO> Create([FromBody] AbsenceTimelineDTO absenceDto)
    {
        var absence = _mapper.Map<AbsenceTimeline>(absenceDto);
        var createdAbsence = _absenceTimelineService.Create(absence);
        var absenceReadDto = _mapper.Map<AbsenceTimelineReadDTO>(createdAbsence);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/absences/{absence.Id}";

        return Created(locationUri, absenceReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] AbsenceTimelineDTO absenceDto)
    {
        if (!Guid.TryParse(id, out var objectId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(objectId);
        if (absence == null)
            return NotFound();

        _mapper.Map(absenceDto, absence);
        _absenceTimelineService.Update(absence);
        var outputDto = _mapper.Map<AbsenceTimelineReadDTO>(absence);

        return Ok(outputDto);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var absence = _absenceTimelineService.SingleByIdOrDefault(guidId);
        if (absence == null)
            return NotFound();

        _absenceTimelineService.Delete(guidId);

        return NoContent();
    }
}