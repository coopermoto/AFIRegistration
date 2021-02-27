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
        public void Should_have_error_when_FirstName_is_null()
        {
            var model = new CustomerDTO {FirstName = null};

            var result = validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name is required");
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
    }
}
