using FluentValidation;
using TeacherTracker.Application.Features.Teachers.DTOs;

namespace TeacherTracker.Application.Features.Teachers.Validators;
public class UpdateTeacherRequestValidator: AbstractValidator<UpdateTeacherRequestDto> {
    public UpdateTeacherRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MaximumLength(100).WithMessage("El nombre del Teacher es muy largo");
    }
}