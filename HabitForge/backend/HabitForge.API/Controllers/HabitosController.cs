using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using HabitForge.Application.Interfaces;
using HabitForge.Domain.Entities;
using HabitForge.Application.Features.Habitos.DTOs;

namespace HabitForge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitosController : ControllerBase {
    private readonly IHabitoRepository _repository;
    private readonly IValidator<CreateHabitoRequestDto> _validator;
    private readonly IValidator<UpdateHabitoRequestDto> _validatorUpdate;

    public HabitosController(IHabitoRepository repository, IValidator<CreateHabitoRequestDto> validator) {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetHabitos() {
        var habitos = await _repository.GetAllAsync();
        
        var response = habitos.Select(h => new HabitoResponseDto {
            Id = h.Id,
            Nombre = h.Nombre
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearHabito([FromBody] CreateHabitoRequestDto request) {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors);
        }
        
        var nuevoHabito = new HabitoGlobal {
            Nombre = request.Nombre
        };


        var habitoCreado = await _repository.AddAsync(nuevoHabito);
        
        var response = new HabitoResponseDto {
            Id = habitoCreado.Id,
            Nombre = habitoCreado.Nombre
        };

        return CreatedAtAction(nameof(GetHabitos), new { id = response.Id }, response);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int:id)
    {
        var habito = await _repository.GetByIdAynce(id);
        if (habito is null)
        {
            return NotFound();
        }
        
        return OK(habito);
    }

    [HtppPut]
    public async Task<IActionResult> Update(int id, UpdateHabitoRequestDto request) {
        var habito = await _repository.GetByIdAsync(id);
        if (habito is null)
        {
            return NotFound();
        }
        
        var validation = await _updateValidator.ValidateAsync(request);
        if (!validation.IsValid) {
            return BadRequest(validationResult.Errors);
        }
        habito.Nombre = request.Nombre
            await _repository.UpdateAsync(habito);
            return CreatedAtAction(nameof(GetHabitos), new { id = response.Id }, response);
    }
    
    
}    

