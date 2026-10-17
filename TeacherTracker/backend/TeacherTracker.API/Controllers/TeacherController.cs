using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TeacherTracker.Application.Features.Teachers.DTOs;
using TeacherTracker.Application.Interfaces;
using TeacherTracker.Domain.Entities;

namespace TeacherTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase {
    private readonly ITeacherRepository _repository;
    private readonly IValidator<CreateTeacherRequestDto> _createValidator;
    private readonly IValidator<UpdateTeacherRequestDto> _updateValidator;

    public TeachersController(
        ITeacherRepository repository,
        IValidator<CreateTeacherRequestDto> createValidator,
        IValidator<UpdateTeacherRequestDto> updateValidator) {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers() {
        var teachers = await _repository.GetAllAsync();

        var response = teachers.Select(t => new TeacherResponseDto {
            Id = t.Id,
            Nombre = t.Nombre
        });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher is null) {
            return NotFound();
        }

        return Ok(new TeacherResponseDto {
            Id = teacher.Id,
            Nombre = teacher.Nombre
        });
    }

    [HttpPost]
    public async Task<IActionResult> CrearTeacher([FromBody] CreateTeacherRequestDto request) {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var nuevoTeacher = new TeacherGlobal {
            Nombre = request.Nombre
        };

        var teacherCreated = await _repository.AddAsync(nuevoTeacher);

        var response = new TeacherResponseDto {
            Id = teacherCreated.Id,
            Nombre = teacherCreated.Nombre
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarTeacher(int id, [FromBody] UpdateTeacherRequestDto request) {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher is null) {
            return NotFound();
        }

        var validation = await _updateValidator.ValidateAsync(request);
        if (!validation.IsValid) {
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary()));
        }

        teacher.Nombre = request.Nombre;
        await _repository.UpdateAsync(teacher);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarTeacher(int id) {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted) {
            return NotFound();
        }

        return Ok(new { message = "Eliminado con éxito" });
    }
}
