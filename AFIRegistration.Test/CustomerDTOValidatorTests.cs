using AFIRegistration.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;

namespace AFIRegistration.Test
{
    [TestFixture]
    public class CustomerDTOValidatorTests
    {
        private CustomerDTOValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CustomerDTOValidator();
        }

        [Test]
        public void Should_have_no_errors_when_valid()
        {
            var model = new CustomerDTO
            {
                FirstName = "Rob",
                Surname = "Carson",
                PolicyNumber = "AF-123456",
                EmailAddress = "robcarson@animalfriends.co.uk"
            };

            var result = _validator.TestValidate(model);

            Assert.IsTrue(result.IsValid);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Should_have_errors_on_all_properties_when_empty()
        {
            var model = new CustomerDTO();

            var result = _validator.TestValidate(model);

            Assert.IsFalse(result.IsValid);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.Surname);
            result.ShouldHaveValidationErrorFor(x => x.PolicyNumber);
            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Test]
        public void Should_have_error_when_FirstName_missing()
        {
            var model = new CustomerDTO {FirstName = null};

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name is required");
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_FirstName_invalid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name should be between 3 and 50 characters");
        }

        [TestCase("Rob")]
        [TestCase("Scott")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_FirstName_valid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void Should_have_error_when_Surname_missing()
        {
            var model = new CustomerDTO {Surname = null};

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname is required");
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_Surname_invalid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname should be between 3 and 50 characters");
        }

        [TestCase("Carson")]
        [TestCase("Rickman")]
        [TestCase("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_Surname_valid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Surname);
        }

        [Test]
        public void Should_have_error_when_PolicyNumber_missing()
        {
            var model = new CustomerDTO {PolicyNumber = null};

            var result = _validator.TestValidate(model);

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
        public void Should_have_error_when_PolicyNumber_invalid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PolicyNumber)
                .WithErrorMessage("Policy Number is not valid");
        }

        [TestCase("AZ-123456")]
        [TestCase("ZA-654321")]
        [TestCase("AA-000000")]
        [TestCase("ZZ-999999")]
        public void Should_not_have_error_when_PolicyNumber_valid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.PolicyNumber);
        }

        [Test]
        public void Should_have_error_when_DateOfBirth_and_EmailAddress_missing()
        {
            var model = new CustomerDTO
            {
                DateOfBirth = null,
                EmailAddress = null
            };

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                .WithErrorMessage("Either Date of Birth or Email Address is required");

            result
                .ShouldHaveValidationErrorFor(x => x.EmailAddress)
                .WithErrorMessage("Either Email Address or Date of Birth is required");
        }

        [Test]
        public void Should_not_have_error_on_DateOfBirth_when_EmailAddress_present()
        {
            var model = new CustomerDTO
            {
                DateOfBirth = null,
                EmailAddress = "robcarson@afi.co.uk"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Test]
        public void Should_not_have_error_on_EmailAddress_when_DateOfBirth_present()
        {
            var model = new CustomerDTO
            {
                DateOfBirth = DateTime.Now,
                EmailAddress = null
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.EmailAddress);
        }

        public class DateOfBirthTestData
        {
            public DateTime DateOfBirth{ get; init; }
            public bool ExpectedResult{ get; init; }
        }

        private static DateOfBirthTestData[] _dateOfBirthTestData =
        {
            new() {DateOfBirth = DateTime.Now, ExpectedResult = false},
            new() {DateOfBirth = DateTime.Now.AddYears(18), ExpectedResult = false},
            new() {DateOfBirth = DateTime.Now.AddYears(-17), ExpectedResult = false},
            new() {DateOfBirth = DateTime.Now.AddYears(-18).AddSeconds(1), ExpectedResult = false},
            new() {DateOfBirth = DateTime.Now.AddYears(-18), ExpectedResult = true},
            new() {DateOfBirth = DateTime.Now.AddYears(-50), ExpectedResult = true},
            new() {DateOfBirth = DateTime.Now.AddYears(-99), ExpectedResult = true},
        };

        [Test]
        public void Should_validate_DateOfBirth_when_present([ValueSource(nameof(_dateOfBirthTestData))]DateOfBirthTestData testData)
        {
            var model = new CustomerDTO {DateOfBirth = testData.DateOfBirth};

            var result = _validator.TestValidate(model);

            if (testData.ExpectedResult)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.DateOfBirth);
            }
            else
            {
                result
                    .ShouldHaveValidationErrorFor(x => x.DateOfBirth)
                    .WithErrorMessage("Customer must be at least 18");
            }
        }

        [TestCase("aaa@a.com", false)]
        [TestCase("aaaa@a.com", false)]
        [TestCase("aaa@aa.com", false)]
        [TestCase("aaaa@aa.com", true)]
        [TestCase("111@1.com", false)]
        [TestCase("1111@1.com", false)]
        [TestCase("111@11.com", false)]
        [TestCase("1111@11.com", true)]
        [TestCase("aaaa@aa.net", false)]
        [TestCase("aaaa@aa.co.uk", true)]
        [TestCase("aaaaaaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaa.com", true)]
        [TestCase("robcarson@afi.com", true)]
        [TestCase("scottrickman@afi.com", true)]
        [TestCase("rob.carson@afi.com", false)]
        [TestCase("scott_rickman@afi.com", false)]
        public void Should_validate_EmailAddress_when_present(string emailAddress, bool expectedResult)
        {
            var model = new CustomerDTO {EmailAddress = emailAddress};

            var result = _validator.TestValidate(model);

            if (expectedResult)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.EmailAddress);
            }
            else
            {
                result
                    .ShouldHaveValidationErrorFor(x => x.EmailAddress)
                    .WithErrorMessage("Email Address is not valid");
            }
        }
    }
}
