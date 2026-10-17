using FluentValidation;
using HabitForge.Application.Features.Habitos.DTOs;

namespace HabitForge.Application.Features.Habitos.Validators;
public class UpdateHabitoRequestValidator: AbstraactValidator<UpdateHabitoRequestDto> {
    public UpdateHabitoRequestValidator()
    {
        Rulefor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MaximunLength(100).WithMessage("El nombre del habito es muy largo");
    }
    
    
}