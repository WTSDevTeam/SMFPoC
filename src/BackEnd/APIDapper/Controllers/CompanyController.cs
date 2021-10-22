using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniDemo.Model;

namespace MiniDemo.Controllers;


[Route("api/company")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepo;

    public CompaniesController(ICompanyRepository companyRepo)
    {
        _companyRepo = companyRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompany(id);
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

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
    {
        try
        {
            var createdCompany = await _companyRepo.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, CompanyForCreationDto company)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepo.UpdateCompany(id, company);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepo.DeleteCompany(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


}