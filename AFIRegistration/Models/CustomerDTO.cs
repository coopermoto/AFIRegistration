using FluentValidation;
using System;

namespace AFIRegistration.Models
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string EmailAddress { get; set; }
    }

    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public const int FirstNameMinLength = 3;
        public const int FirstNameMaxLength = 50;
        public const int SurnameMinLength = 3;
        public const int SurnameMaxLength = 50;
        public const string PolicyNumberFormat = @"^[A-Z]{2}-\d{6}$";
        public const string EmailAddressFormat = @"^\w{4,}@\w{2,}(?:.com|.co.uk)$";

        public CustomerDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                    .WithMessage("First Name is required")
                .Length(FirstNameMinLength, FirstNameMaxLength)
                    .WithMessage($"First Name should be between {FirstNameMinLength} and {FirstNameMaxLength} characters");

            RuleFor(x => x.Surname)
                .NotNull()
                    .WithMessage("Surname is required")
                .Length(SurnameMinLength, SurnameMaxLength)
                    .WithMessage($"Surname should be between {SurnameMinLength} and {SurnameMaxLength} characters");

            RuleFor(x => x.PolicyNumber)
                .NotNull()
                    .WithMessage("Policy Number is required")
                .Matches(PolicyNumberFormat)
                    .WithMessage("Policy Number is not valid");

            RuleFor(x => x.DateOfBirth)
                .NotNull().When(x => x.EmailAddress == null)
                    .WithMessage("Either Date of Birth or Email Address is required")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                    .WithMessage("Customer must be at least 18");

            RuleFor(x => x.EmailAddress)
                .NotNull().When(x => x.DateOfBirth == null)
                    .WithMessage("Either Email Address or Date of Birth is required")
                .Matches(EmailAddressFormat)
                    .WithMessage("Email Address is not valid");
        }
    }
}
