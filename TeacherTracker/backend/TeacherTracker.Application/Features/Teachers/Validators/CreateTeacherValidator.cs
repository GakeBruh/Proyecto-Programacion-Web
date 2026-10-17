using FluentValidation;
using TeacherTracker.Application.Features.Teachers.DTOs;

namespace TeacherTracker.Application.Features.Teachers.Validators;

public class CreateTeacherValidator : AbstractValidator<CreateTeacherRequestDto> {
    public CreateTeacherValidator() {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del hábito no puede estar vacío.")
            .MinimumLength(3).WithMessage("El hábito debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El hábito es demasiado largo.");
    }
}