using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.DTOs;

namespace Yaqeen.Application.Validators
{
    public class ValidateCitizenRequestDtoValidator  : AbstractValidator<ValidateCitizenRequestDto>
    {
        public ValidateCitizenRequestDtoValidator() {

            RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required.")
            .Matches(@"^\d{10}$").WithMessage("National ID must be exactly 10 digits.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.");
        }
    }
}
