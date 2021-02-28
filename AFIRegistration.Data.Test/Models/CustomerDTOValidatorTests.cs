using AFIRegistration.Data.Models;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AFIRegistration.Data.Test.Models
{
    [TestClass]
    public class CustomerDTOValidatorTests
    {
        private CustomerDTOValidator _validator;

        [TestInitialize]
        public void TestInitialise()
        {
            _validator = new CustomerDTOValidator();
        }
         
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void Should_have_error_when_FirstName_missing()
        {
            var model = new CustomerDTO {FirstName = null};

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name is required");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("aa")]
        [DataRow("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_FirstName_invalid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name should be between 3 and 50 characters");
        }

        [DataTestMethod]
        [DataRow("Rob")]
        [DataRow("Scott")]
        [DataRow("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_FirstName_valid(string firstName)
        {
            var model = new CustomerDTO {FirstName = firstName};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [TestMethod]
        public void Should_have_error_when_Surname_missing()
        {
            var model = new CustomerDTO {Surname = null};

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname is required");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("aa")]
        [DataRow("1234567890123456789012345678901234567890123456789012")]
        public void Should_have_error_when_Surname_invalid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Surname)
                .WithErrorMessage("Surname should be between 3 and 50 characters");
        }

        [DataTestMethod]
        [DataRow("Carson")]
        [DataRow("Rickman")]
        [DataRow("12345678901234567890123456789012345678901234567890")]
        public void Should_not_have_error_when_Surname_valid(string surname)
        {
            var model = new CustomerDTO {Surname = surname};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Surname);
        }

        [TestMethod]
        public void Should_have_error_when_PolicyNumber_missing()
        {
            var model = new CustomerDTO {PolicyNumber = null};

            var result = _validator.TestValidate(model);

            result
                .ShouldHaveValidationErrorFor(x => x.PolicyNumber)
                .WithErrorMessage("Policy Number is required");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("aa")]
        [DataRow("aa-123456")]
        [DataRow("99-123456")]
        [DataRow("AA-12345")]
        [DataRow("AA-1234567")]
        [DataRow("AA-AAAAAA")]
        [DataRow("AA 123456")]
        [DataRow("AA_123456")]
        public void Should_have_error_when_PolicyNumber_invalid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.PolicyNumber)
                .WithErrorMessage("Policy Number is not valid");
        }

        [DataTestMethod]
        [DataRow("AZ-123456")]
        [DataRow("ZA-654321")]
        [DataRow("AA-000000")]
        [DataRow("ZZ-999999")]
        public void Should_not_have_error_when_PolicyNumber_valid(string policyNumber)
        {
            var model = new CustomerDTO {PolicyNumber = policyNumber};

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.PolicyNumber);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [DataTestMethod]
        [DynamicData(nameof(GetDateOfBirthTestData), DynamicDataSourceType.Method)]
        public void Should_validate_DateOfBirth_when_present(DateTime dateOfBirth, bool expectedResult)
        {
            var model = new CustomerDTO {DateOfBirth = dateOfBirth};

            var result = _validator.TestValidate(model);

            if (expectedResult)
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

        [DataTestMethod]
        [DataRow("aaa@a.com", false)]
        [DataRow("aaaa@a.com", false)]
        [DataRow("aaa@aa.com", false)]
        [DataRow("aaaa@aa.com", true)]
        [DataRow("111@1.com", false)]
        [DataRow("1111@1.com", false)]
        [DataRow("111@11.com", false)]
        [DataRow("1111@11.com", true)]
        [DataRow("aaaa@aa.net", false)]
        [DataRow("aaaa@aa.co.uk", true)]
        [DataRow("aaaaaaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaa.com", true)]
        [DataRow("robcarson@afi.com", true)]
        [DataRow("scottrickman@afi.com", true)]
        [DataRow("rob.carson@afi.com", false)]
        [DataRow("scott_rickman@afi.com", false)]
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

        private static IEnumerable<object[]> GetDateOfBirthTestData()
        {
            yield return new object[] {DateTime.Now, false};
            yield return new object[] {DateTime.Now.AddYears(18), false};
            yield return new object[] {DateTime.Now.AddYears(-17), false};
            yield return new object[] {DateTime.Now.AddYears(-18).AddSeconds(1), false};
            yield return new object[] {DateTime.Now.AddYears(-18), true};
            yield return new object[] {DateTime.Now.AddYears(-50), true};
            yield return new object[] {DateTime.Now.AddYears(-99), true};
        }
    }
}
