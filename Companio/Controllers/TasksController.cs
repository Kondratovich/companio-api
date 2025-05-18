using AutoMapper;
using Companio.DTO;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Task = Companio.Models.Task;

namespace Companio.Controllers;

public class TasksController : Controller
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;

    public TasksController(IMapper mapper, ITaskService taskService)
    {
        _mapper = mapper;
        _taskService = taskService;
    }

    [HttpGet("api/v1/tasks")]
    public ActionResult<List<TaskReadDTO>> GetAll()
    {
        var tasks = _taskService.GetAll();
        var taskReadDtos = tasks.Select(_mapper.Map<TaskReadDTO>);
        return Ok(taskReadDtos);
    }

    [HttpGet("api/v1/tasks/{id}")]
    public ActionResult<TaskReadDTO> Get(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var task = _taskService.SingleByIdOrDefault(objectId);

        if (task == null)
            return NotFound();

        var taskReadDto = _mapper.Map<TaskReadDTO>(task);

        return Ok(taskReadDto);
    }

    [HttpPost("api/v1/tasks")]
    public ActionResult<TaskReadDTO> Create([FromBody] TaskDTO taskDto)
    {
        var task = _mapper.Map<Task>(taskDto);
        var createdTask = _taskService.Create(task);
        var taskReadDto = _mapper.Map<TaskReadDTO>(createdTask);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/tasks/{task.Id}";

        return Created(locationUri, taskReadDto);
    }

    [HttpPut("api/v1/tasks/{id}")]
    public ActionResult Put(string id, [FromBody] TaskDTO taskDto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var task = _taskService.SingleByIdOrDefault(objectId);
        if (task == null)
            return NotFound();

        _mapper.Map(taskDto, task);
        _taskService.Update(task);
        var outputDto = _mapper.Map<TaskReadDTO>(task);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/tasks/{id}")]
    public ActionResult Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var task = _taskService.SingleByIdOrDefault(objectId);
        if (task == null)
            return NotFound();

        _taskService.Delete(objectId);

        return NoContent();
    }
}