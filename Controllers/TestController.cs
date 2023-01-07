using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

public class TestController : Controller
{
    [HttpGet("api/user")]
    public IActionResult Get ()
    {
        return Ok(new { name = "Artsem" });
    }
}