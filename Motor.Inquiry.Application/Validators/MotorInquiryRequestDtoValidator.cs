using FluentValidation;
using Motor.Inquiry.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Motor.Inquiry.Application.Validators
{
    public class MotorInquiryRequestDtoValidator : AbstractValidator<MotorInquiryRequestDto>
    {
        public MotorInquiryRequestDtoValidator()
        {

            RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required.")
            .Matches(@"^\d{10}$").WithMessage("National ID must be exactly 10 digits.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x)
                .Must(HaveSequenceOrPlateOnly)
                .WithMessage("You must provide either Sequence Number OR Plate details, not both.");

            When(x => !string.IsNullOrWhiteSpace(x.SequenceNumber), () =>
            {
                RuleFor(x => x.SequenceNumber)
                    .Matches(@"^\d{9,10}$").WithMessage("Sequence number must be between 9 and 10 digits.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.PlateLetters), () =>
            {
                RuleFor(x => x.PlateLetters)
                    .Length(3).WithMessage("Plate letters must be exactly 3 characters.");

                RuleFor(x => x.PlateNumber)
                    .NotEmpty().WithMessage("Plate number is required when using plate inquiry.").Matches(@"^\d{1,4}$").WithMessage("Plate number must be between 1 and 4 digits.");
            });
        }

    
    private bool HaveSequenceOrPlateOnly(MotorInquiryRequestDto dto)
        {
            bool hasSequence = !string.IsNullOrWhiteSpace(dto.SequenceNumber);
            bool hasPlate = !string.IsNullOrWhiteSpace(dto.PlateLetters) && !string.IsNullOrWhiteSpace(dto.PlateNumber);

            // Logical XOR: returns true if only ONE option is provided
            return hasSequence ^ hasPlate;
        }
    }
}

