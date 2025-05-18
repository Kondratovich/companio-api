using AutoMapper;
using Companio.DTO;
using Companio.Models;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

public class CustomersController : Controller
{
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;

    public CustomersController(IMapper mapper, ICustomerService customerService)
    {
        _mapper = mapper;
        _customerService = customerService;
    }

    [HttpGet("api/v1/customers")]
    public ActionResult<List<CustomerReadDTO>> GetAll()
    {
        var customers = _customerService.GetAll();
        var customerReadDtos = customers.Select(_mapper.Map<CustomerReadDTO>);
        return Ok(customerReadDtos);
    }

    [HttpGet("api/v1/customers/{id}")]
    public ActionResult<CustomerReadDTO> Get(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var customer = _customerService.SingleByIdOrDefault(guidId);

        if (customer == null)
            return NotFound();

        var customerReadDto = _mapper.Map<CustomerReadDTO>(customer);

        return Ok(customerReadDto);
    }

    [HttpPost("api/v1/customers")]
    public ActionResult<CustomerReadDTO> Create([FromBody] CustomerDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        var createdCustomer = _customerService.Create(customer);
        var customerReadDto = _mapper.Map<CustomerReadDTO>(createdCustomer);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + $"/api/v1/customers/{customer.Id}";

        return Created(locationUri, customerReadDto);
    }

    [HttpPut("api/v1/customers/{id}")]
    public ActionResult Put(string id, [FromBody] CustomerDTO customerDto)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var customer = _customerService.SingleByIdOrDefault(guidId);
        if (customer == null)
            return NotFound();

        _mapper.Map(customerDto, customer);
        _customerService.Update(customer);
        var outputDto = _mapper.Map<CustomerReadDTO>(customer);

        return Ok(outputDto);
    }

    [HttpDelete("api/v1/customers/{id}")]
    public ActionResult Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
            return ValidationProblem();

        var customer = _customerService.SingleByIdOrDefault(guidId);
        if (customer == null)
            return NotFound();

        _customerService.Delete(guidId);

        return NoContent();
    }
}