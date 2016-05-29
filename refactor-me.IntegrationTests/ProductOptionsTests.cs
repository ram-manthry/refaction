using System;
using System.Collections.Generic;
using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;
using refactor_me.Controllers;
using refactor_me.Models;

namespace refactor_me.IntegrationTests
{
    [TestFixture]
    public class ProductOptionsTests
    {
        [SetUp]
        public void SetUp()
        {
            _productsController = new ProductsController();
            _productOptionIds = new List<Guid>();
        }

        private ProductsController _productsController;
        private List<Guid> _productOptionIds;
      
        [TearDown]
        public void TearDown()
        {
            foreach (var productOptionId in _productOptionIds)
            {
                _productsController.DeleteOption(productOptionId);
            }
        }


        [Test]
        public void CreateOption_GetOption()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var expected = new ProductOption()
            {
                Id = optionId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                ProductId = productId
            };
            _productsController.CreateOption(productId,expected);

            _productOptionIds.Add(optionId);
            
            //Act
            var actual = _productsController.GetOption(productId,optionId);

            //Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void DeleteOption()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var expected = new ProductOption()
            {
                Id = optionId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                ProductId = productId
            };
            _productsController.CreateOption(productId, expected);
            
            //Act
            _productsController.DeleteOption(optionId);
           
            //Assert
            _productsController.Invoking(controller => controller.GetOption(productId, optionId))
                .ShouldThrow<HttpResponseException>();
        }

        [Test]
        public void GetOptions()
        {
            //Arrange
            var productId = Guid.NewGuid();

            var optionId1 = Guid.NewGuid();
            var option1 = new ProductOption()
            {
                Id = optionId1,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                ProductId = productId
            };

            var optionId2 = Guid.NewGuid();
            var option2 = new ProductOption()
            {
                Id = optionId2,
                Name = "Dummy Name 2",
                Description = "Dummy Description 2",
                ProductId = productId
            };

            _productsController.CreateOption(productId, option1);
            _productsController.CreateOption(productId, option2);

            _productOptionIds.Add(optionId1);
            _productOptionIds.Add(optionId2);

            //Act
            var actual = _productsController.GetOptions(productId);

            //Assert
            actual.Items.Should().Contain(option1);
            actual.Items.Should().Contain(option2);
        }

        [Test]
        public void UpdateOption()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var option = new ProductOption()
            {
                Id = optionId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                ProductId = productId
            };
            _productsController.CreateOption(productId, option);

            _productOptionIds.Add(optionId);

            var expected = option;
            expected.Name = "Dummy Name 1 updated";

            //Act
            _productsController.UpdateOption(optionId,expected);
            var actual = _productsController.GetOption(productId, optionId);

            //Assert
            actual.Should().Be(option);
        }

        [Test]
        public void UpdateOption_When_Option_Doesnt_Exist_Throws_HttpResponse_Exception()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var option = new ProductOption()
            {
                Id = optionId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                ProductId = productId
            };
            
            var expected = option;
            expected.Name = "Dummy Name 1 updated";

            //Act
            _productsController.Invoking(controller => controller.UpdateOption(optionId, option))
                .ShouldThrow<HttpResponseException>();
        }
    }
}