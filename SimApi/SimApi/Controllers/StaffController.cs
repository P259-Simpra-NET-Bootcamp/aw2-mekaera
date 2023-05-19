using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimApi.Data.Domain;
using SimApi.Data.Repository;
using SimApi.Schema;
using SimApi.Service.Validators;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IStaffRepository repository;
    private IMapper mapper;
    private readonly IValidator<Staff> uservalidator;
    public StaffController(IMapper mapper, IStaffRepository repository, IValidator<Staff> uservalidator)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.uservalidator = uservalidator;
    }


    [HttpGet]
    public List<StaffResponse> GetAll()
    {
        var list = repository.GetAll();
        var mapped = mapper.Map<List<StaffResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public StaffResponse GetById(int id)
    {
        var row = repository.GetById(id);
        var mapped = mapper.Map<StaffResponse>(row);
        return mapped;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] StaffRequest request)
    {
        var entity = mapper.Map<Staff>(request); 
        if (repository.FindByEmail(entity.Email).Count == 0)
        {
            ValidationResult result = await uservalidator.ValidateAsync(entity);

            if (!result.IsValid)
            {
                Console.WriteLine("Bir sorun oluştu sanırsam");
                result.AddToModelState(this.ModelState);


                return BadRequest(ModelState);

            }

            repository.Insert(entity);
            repository.Complete();
            return Ok();
        }
        else
        {
            return BadRequest("Email kullanılıyor");
            Console.WriteLine("Email kullanılıyor");
        }
       

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] StaffResponse request)
    {

        var entity = new Staff();
        entity.Id = id;
        entity.Email = request.Email;
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Province = request.Province;
        entity.City = request.City;
        entity.AddressLine1 = request.AddressLine1;
        entity.Country = request.Country;
        entity.DateOfBirth = request.DateOfBirth;
        entity.Phone = request.Phone;
        entity.CreatedBy = "sim@sim.com";
        if (repository.FindByEmail(entity.Email).Count == 0)
        {
            ValidationResult result = await uservalidator.ValidateAsync(entity);

            if (!result.IsValid)
            {
                Console.WriteLine("Bir sorun oluştu sanırsam");
                result.AddToModelState(this.ModelState);


                return BadRequest(ModelState);

            }

            repository.Update(entity);
            repository.Complete();
            return Ok();
        }
        else
        {
            return BadRequest("Email kullanılıyor");
            Console.WriteLine("Email kullanılıyor");
        }

    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        repository.DeleteById(id);
        repository.Complete();
    }
    [HttpGet("filterwithname/{name}")]
    public List<StaffResponse> GetFilteredNames(string name)
    {
        var list = repository.FindByName(name);
        var mapped = mapper.Map<List<StaffResponse>>(list);
        return mapped;
    }
    [HttpGet("filterwithlocation/{location}")]
    public List<StaffResponse> GetFilteredLocation(string location)
    {
     
        var list = repository.FindByLocation(location);
       
        var mapped = mapper.Map<List<StaffResponse>>(list);
        
        return mapped;
      
    }
    [HttpGet("filterwithlocationandname")]
    public List<StaffResponse> GetFilteredLocation([FromQuery] string location, [FromQuery] string name)
    {
        var list = repository.FindByName(name);
        var list2 = repository.FindByLocation(location);
        var intersection = list.Intersect(list2);
        var mapped = mapper.Map<List<StaffResponse>>(intersection);

        return mapped;

    }
}
