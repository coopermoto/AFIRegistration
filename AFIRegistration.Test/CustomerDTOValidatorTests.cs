using AFIRegistration.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace AFIRegistration.Test
{
    [TestFixture]
    public class CustomerDTOValidatorTests
    {
        private CustomerDTOValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new CustomerDTOValidator();
        }

        [Test]
        public void Should_have_error_when_FirstName_is_missing()
        {
            var model = new CustomerDTO {FirstName = null};

            var result = validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name is required");
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_FirstName_is_invalid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name should be between 3 and 50 characters");
        }

        [TestCase("Rob")]
        [TestCase("Scott")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_FirstName_is_valid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void Should_have_error_when_Surname_is_missing()
        {
            var model = new CustomerDTO {Surname = null};

            var result = validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname is required");
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_Surname_is_invalid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname should be between 3 and 50 characters");
        }

        [TestCase("Carson")]
        [TestCase("Rickman")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_Surname_is_valid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Surname);
        }

        [Test]
        public void Should_have_error_when_PolicyNumber_is_missing()
        {
            var model = new CustomerDTO {PolicyNumber = null};

            var result = validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.PolicyNumber)
                .WithErrorMessage("Policy Number is required");
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("aa-123456")]
        [TestCase("99-123456")]
        [TestCase("AA-12345")]
        [TestCase("AA-1234567")]
        [TestCase("AA-AAAAAA")]
        [TestCase("AA 123456")]
        [TestCase("AA_123456")]
        public void Should_have_error_when_PolicyNumber_is_invalid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PolicyNumber)
                .WithErrorMessage("Policy Number is not valid");
        }

        [TestCase("AZ-123456")]
        [TestCase("ZA-654321")]
        [TestCase("AA-000000")]
        [TestCase("ZZ-999999")]
        public void Should_not_have_error_when_PolicyNumber_is_valid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.PolicyNumber);
        }
    }
}
