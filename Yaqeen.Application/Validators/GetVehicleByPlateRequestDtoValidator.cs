using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.DTOs;

namespace Yaqeen.Application.Validators
{
    public class GetVehicleByPlateRequestDtoValidator : AbstractValidator<GetVehicleByPlateRequestDto>
    {
        public GetVehicleByPlateRequestDtoValidator()
        {
            RuleFor(x => x.PlateLetters)
                .NotEmpty().WithMessage("Plate letters are required.")
                .Length(3).WithMessage("Plate letters must be exactly 3 characters.");

            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Plate number is required.")
                .Matches(@"^\d{1,4}$").WithMessage("Plate number must be between 1 and 4 digits.");
        }
    }
}
