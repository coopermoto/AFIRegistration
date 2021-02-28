using AFIRegistration.Data.Models;
using AFIRegistration.Data.Repositories;
using AutoFixture;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AFIRegistration.Data.Test.Repositories
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        public DbContextOptions<RegistrationContext> InMemoryOptions { get; } = new DbContextOptionsBuilder<RegistrationContext>().UseInMemoryDatabase("TestCustomerList").Options;
        public DbContextOptions<RegistrationContext> DummyOptions { get; } = new DbContextOptionsBuilder<RegistrationContext>().Options;

        private const int CustomerIdRob = 1;
        private const int CustomerIdScott = 2;
        private const int CustomerIdNew = 3;
        private Fixture _fixture;

        [TestInitialize]
        public void TestInitialise()
        {
            _fixture = new Fixture();

            SeedDatabase();
        }

        [TestMethod]
        public async Task GetAllCustomersAsync_ReturnsCorrectResult()
        {
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var expectedResults = await context.Customers.ToListAsync();

                // Act
                var actualResults = await repository.GetAllCustomersAsync();

                // Assert
                CollectionAssert.AreEqual(expectedResults, actualResults);
            }
        }

        [TestMethod]
        public async Task GetCustomerByIdAsync_ReturnsCorrectResult()
        {
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var expectedResult = context.Customers.Single(x => x.CustomerId == CustomerIdRob);

                // Act
                var actualResult = await repository.GetCustomerByIdAsync(CustomerIdRob);

                // Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public void GetAll_ReturnsCorrectResult()
        {
            using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var expectedResults = context.Customers.ToListAsync().Result;

                // Act
                var actualResults = repository.GetAll().ToList();

                // Assert
                CollectionAssert.AreEqual(expectedResults, actualResults);
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenValid_AddsCustomer()
        {
            var newCustomer = _fixture.Build<Customer>()
                .Without(x => x.CustomerId)
                .Create();

            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);

                // Act
                await repository.AddAsync(newCustomer);
            }

            // Assert
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                var customer = context.Customers.Single(x => x.CustomerId == CustomerIdNew);

                Assert.AreEqual(CustomerIdNew, customer.CustomerId);
                Assert.AreEqual(newCustomer.FirstName, customer.FirstName);
                Assert.AreEqual(newCustomer.Surname, customer.Surname);
                Assert.AreEqual(newCustomer.PolicyNumber, customer.PolicyNumber);
                Assert.AreEqual(newCustomer.DateOfBirth, customer.DateOfBirth);
                Assert.AreEqual(newCustomer.EmailAddress, customer.EmailAddress);
                Assert.AreEqual(newCustomer.Created, customer.Created);
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenValid_ReturnsCorrectResult()
        {
            var newCustomer = _fixture.Build<Customer>()
                .Without(x => x.CustomerId)
                .Create();

            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var expectedResult = newCustomer;
                expectedResult.CustomerId = CustomerIdNew;

                // Act
                var actualResult = await repository.AddAsync(newCustomer);

                // Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenNullCustomer_ThrowsArgumentNullException()
        {
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);

                // Act
                var ex = await AssertEx.ThrowsAsync<ArgumentNullException>(() => repository.AddAsync(null));

                // Assert
                Assert.AreEqual("AddAsync entity must not be null", ex.ParamName);
            }
        }

        [TestMethod]
        public async Task AddAsync_WhenException_ThrowsException()
        {
            // Arrange
            var mockContext = new DbContextMock<RegistrationContext>(DummyOptions);
            mockContext.Setup(x => x.SaveChangesAsync(default))
                .Callback(() => throw new Exception());
            var repository = new CustomerRepository(mockContext.Object);
            var newCustomer = _fixture.Build<Customer>()
                .Without(x => x.CustomerId)
                .Create();
            const string expectedMessage = "entity could not be saved";

            // Act
            var ex = await AssertEx.ThrowsAsync<Exception>(() => repository.AddAsync(newCustomer));

            // Assert
            Assert.AreEqual(expectedMessage, ex.Message.Substring(0, expectedMessage.Length));
        }

        [TestMethod]
        public async Task UpdateAsync_WhenValid_UpdatesCustomer()
        {
            const string newEmailAddress = "scottrickman@afi.com";

            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var customer = context.Customers.Single(x => x.CustomerId == CustomerIdScott);
                customer.EmailAddress = newEmailAddress;

                // Act
                await repository.UpdateAsync(customer);
            }

            // Assert
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                var customer = context.Customers.Single(x => x.CustomerId == CustomerIdScott);

                Assert.AreEqual(newEmailAddress, customer.EmailAddress);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_WhenValid_ReturnsCorrectResult()
        {
            const string newEmailAddress = "scottrickman@afi.com";

            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);
                var customer = context.Customers.Single(x => x.CustomerId == CustomerIdScott);
                customer.EmailAddress = newEmailAddress;
                var expectedResult = customer;

                // Act
                var actualResult = await repository.UpdateAsync(customer);

                // Assert
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_WhenNullCustomer_ThrowsArgumentNullException()
        {
            await using (var context = new RegistrationContext(InMemoryOptions))
            {
                // Arrange
                var repository = new CustomerRepository(context);

                // Act
                var ex = await AssertEx.ThrowsAsync<ArgumentNullException>(() => repository.UpdateAsync(null));

                // Assert
                Assert.AreEqual("UpdateAsync entity must not be null", ex.ParamName);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_WhenException_ThrowsException()
        {
            // Arrange
            var mockContext = new DbContextMock<RegistrationContext>(DummyOptions);
            mockContext.Setup(x => x.SaveChangesAsync(default))
                .Callback(() => throw new Exception());
            var repository = new CustomerRepository(mockContext.Object);
            var newCustomer = _fixture.Build<Customer>()
                .Without(x => x.CustomerId)
                .Create();
            const string expectedMessage = "entity could not be updated";

            // Act
            var ex = await AssertEx.ThrowsAsync<Exception>(() => repository.UpdateAsync(newCustomer));

            // Assert
            Assert.AreEqual(expectedMessage, ex.Message.Substring(0, expectedMessage.Length));
        }

        private void SeedDatabase()
        {
            using var context = new RegistrationContext(InMemoryOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Customer
            {
                CustomerId = CustomerIdRob,
                FirstName = "Rob",
                Surname = "Carson",
                PolicyNumber = "AF-123456",
                DateOfBirth = DateTime.Now.AddYears(-21),
                EmailAddress = "robcarson@animalfriends.co.uk"
            });

            context.Add(new Customer
            {
                CustomerId = CustomerIdScott,
                FirstName = "Scott",
                Surname = "Rickman",
                PolicyNumber = "MB-123456",
                DateOfBirth = DateTime.Now.AddYears(-22),
                EmailAddress = "scottrickman@animalfriends.co.uk"
            });

            context.SaveChanges();
        }
    }
}
