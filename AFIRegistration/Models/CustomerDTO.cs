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
        public CustomerDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First Name is required")
                .MinimumLength(3).MaximumLength(60).WithMessage("FirstName should be between 3 and 50 characters");

            RuleFor(x => x.Surname)
                .NotNull().WithMessage("Surname is required")
                .MinimumLength(3).MaximumLength(60).WithMessage("Surname should be between 3 and 50 characters");

            RuleFor(x => x.PolicyNumber)
                .NotNull().WithMessage("Policy Number is required")
                .Matches(@"[A-Z]{2}-\d{6}").WithMessage("Policy Number is not valid");

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Customer must be at least 18");

            RuleFor(x => x.EmailAddress)
                .Matches(@"\w{4,}@\w{2,}(?:.com|.co.uk)").WithMessage("Email Address is not valid");
        }
    }
}
