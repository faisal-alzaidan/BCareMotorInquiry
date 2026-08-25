using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.DTOs;

namespace Yaqeen.Application.Validators
{
    //FluentValidation
    public class VehicleInquiryRequestDtoValidator : AbstractValidator<VehicleInquiryRequestDto>
    {
        public VehicleInquiryRequestDtoValidator()
        {

            RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required.")
            .Matches(@"^\d{10}$").WithMessage("National ID must be exactly 10 digits.");

            RuleFor(x => x.SequenceNumber)
                .NotEmpty().WithMessage("Sequence number is required.")
                .Matches(@"^\d{9,10}$").WithMessage("Sequence number must be between 9 and 10 digits.");

        }
    }
}
