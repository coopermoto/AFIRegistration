using AFIRegistration.Data.Models;
using AFIRegistration.Data.Repositories;
using AFIRegistration.Service.Services;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace AFIRegistration.Service.Test.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private Fixture _fixture;
        private Mock<ICustomerRepository> _mockCustomerRepository;

        [TestInitialize]
        public void TestInitialise()
        {
            _fixture = new Fixture();
            _mockCustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetAllCustomersAsync_CallsRepository_ReturnsCorrectResult()
        {
            // Arrange
            var expectedResults = _fixture.CreateMany<Customer>().ToList();
            _mockCustomerRepository.Setup(x => x.GetAllCustomersAsync().Result).Returns(expectedResults);
            var service = new CustomerService(_mockCustomerRepository.Object);

            // Act
            var actualResults = await service.GetAllCustomersAsync();

            // Assert
            Assert.AreEqual(expectedResults, actualResults);
            _mockCustomerRepository.Verify(x => x.GetAllCustomersAsync(), Times.Once);
            _mockCustomerRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetCustomerByIdAsync_CallsRepository_ReturnsCorrectResult()
        {
            // Arrange
            var customerId = _fixture.Create<int>();
            var expectedResult = _fixture.Create<Customer>();
            _mockCustomerRepository.Setup(x => x.GetCustomerByIdAsync(customerId).Result).Returns(expectedResult);
            var service = new CustomerService(_mockCustomerRepository.Object);

            // Act
            var actualResult = await service.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            _mockCustomerRepository.Verify(x => x.GetCustomerByIdAsync(customerId), Times.Once);
            _mockCustomerRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddCustomerAsync_CallsRepository_ReturnsCorrectResult()
        {
            // Arrange
            var expectedResult = _fixture.Create<Customer>();
            _mockCustomerRepository.Setup(x => x.AddAsync(expectedResult).Result).Returns(expectedResult);
            var service = new CustomerService(_mockCustomerRepository.Object);

            // Act
            var actualResult = await service.AddCustomerAsync(expectedResult);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            _mockCustomerRepository.Verify(x => x.AddAsync(expectedResult), Times.Once);
            _mockCustomerRepository.VerifyNoOtherCalls();
        }
    }
}
