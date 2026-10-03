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
        return Ok(habitos); // HTTP 200 OK
    }
    
    [HttpPost]
    public async Task<IActionResult> CrearHabito([FromBody] HabitoGlobal habito) {
        if (string.IsNullOrWhiteSpace(habito.Nombre)) {
            return BadRequest("El nombre del hábito es obligatorio."); // HTTP 400 Bad Request
        }

        var nuevoHabito = await _repository.AddAsync(habito);
        
        // HTTP 201 Created: Devuelve el recurso recién creado por cortesía RESTful
        return CreatedAtAction(nameof(GetHabitos), new { id = nuevoHabito.Id }, nuevoHabito);
    }
}