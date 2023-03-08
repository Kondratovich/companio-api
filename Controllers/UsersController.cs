using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Companio.Controllers;

public class UsersController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("api/v1/users")]
    public ActionResult<List<UserReadDTO>> GetAll()
    {
        var users = _userService.GetAll();
        var userReadDtos = users.Select(_mapper.Map<UserReadDTO>);
        return Ok(userReadDtos);
    }

    [HttpGet("api/v1/users/{id}")]
    public ActionResult<UserReadDTO> Get(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var user = _userService.SingleByIdOrDefault(objectId);

        if (user == null)
            return NotFound();

        var userReadDto = _mapper.Map<UserReadDTO>(user);

        return Ok(userReadDto);
    }

    [HttpPost("api/v1/users")]
    public ActionResult<UserReadDTO> Create([FromBody] UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var createdUser = _userService.Create(user);
        var userReadDto = _mapper.Map<UserReadDTO>(createdUser);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/projects/{user.Id}";

        return Created(locationUri, userReadDto);
    }

    [HttpPut("api/v1/users/{id}")]
    public ActionResult Put(string id, [FromBody] UserDTO userDto)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var user = _userService.SingleByIdOrDefault(objectId);
        if (user == null)
            return NotFound();

        _mapper.Map(userDto, user);
        _userService.Update(user);
        var outputDto = _mapper.Map<UserReadDTO>(user);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/users/{id}")]
    public ActionResult Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return ValidationProblem();

        var user = _userService.SingleByIdOrDefault(objectId);
        if (user == null)
            return NotFound();

        _userService.Delete(objectId);

        return NoContent();
    }
}