using AFIRegistration.Controllers;
using AFIRegistration.Data.Models;
using AFIRegistration.Service.Services;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AFIRegistration.Test.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        private Fixture _fixture;
        private Mock<ICustomerService> _mockCustomerService;

        [TestInitialize]
        public void TestInitialise()
        {
            _fixture = new Fixture();
            _mockCustomerService = new Mock<ICustomerService>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task PostCustomer_CallsService()
        {
            // Arrange
            var customerDTO = _fixture.Create<CustomerDTO>();
            var customer = _fixture.Create<Customer>();
            _mockCustomerService.Setup(x => x.AddCustomerAsync(It.IsAny<Customer>()).Result).Returns(customer);
            var controller = new CustomersController(_mockCustomerService.Object);

            // Act
            await controller.PostCustomer(customerDTO);

            // Assert
            _mockCustomerService.Verify(x => x.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
            _mockCustomerService.VerifyNoOtherCalls();
        }
    }
}
