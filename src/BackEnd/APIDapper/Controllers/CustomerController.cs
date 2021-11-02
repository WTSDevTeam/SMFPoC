using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniDemo.Model;

namespace MiniDemo.Controllers;


[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _Repo;

    public CustomerController(ICustomerRepository companyRepo)
    {
        _Repo = companyRepo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var data = await _Repo.Get();
            return Ok(data);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var company = await _Repo.Get(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CustomerForCreationDto customer)
    {
        try
        {
            var db = await _Repo.Get(id);
            if (db == null)
                return NotFound();

            await _Repo.Update(id, customer);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }




}