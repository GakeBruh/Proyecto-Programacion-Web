using Microsoft.AspNetCore.Mvc;
using HabitForge.Application.Interfaces;
using HabitForge.Domain.Entities;

namespace HabitForge.API.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class HabitosController : ControllerBase {
    private readonly IHabitoRepository _repository;
    
    public HabitosController(IHabitoRepository repository) {
        _repository = repository; 
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHabitos() {
        var habitos = await _repository.GetAllAsync();
        return Ok(habitos); 
    }
    
    [HttpPost]
    public async Task<IActionResult> CrearHabito([FromBody] HabitoGlobal habito) {
        if (string.IsNullOrWhiteSpace(habito.Nombre)) {
            return BadRequest("El nombre del hábito es obligatorio."); // HTTP 400 Bad Request
        }
        
        var nuevoHabito = await _repository.AddAsync(habito);
        
        return CreatedAtAction(nameof(GetHabitos), new { id = nuevoHabito.Id }, nuevoHabito);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarHabito(int id, [FromBody] UpdateHabitoRequestDto request) {
        var habito = await _repository.GetByIdAsync(id);
        if (habito is null) {
            return NotFound(); 
        }

        var validation = await _updateValidator.ValidateAsync(request);
        if (!validation.IsValid) {
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary())); // HTTP 400
        }

        habito.Nombre = request.Nombre;
        await _repository.UpdateAsync(habito);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarHabito(int id) {
        var delete = await _repository.DeleteAsync(id);
        if (!delete) {
            return NotFound();
        }

        return Message("Eliminado con exito"); 
    }    
    
}