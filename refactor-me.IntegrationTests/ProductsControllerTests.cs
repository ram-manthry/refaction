﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using refactor_me.Controllers;
using refactor_me.Models;


namespace refactor_me.IntegrationTests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            _productsController = new ProductsController();
            _productIds = new List<Guid>();
        }

        private ProductsController _productsController;
        private List<Guid> _productIds;
       
        [TearDown]
        public void TearDown()
        {
            foreach (var productId in _productIds)
            {
                _productsController.Delete(productId);
            }
        }

        [Test]
        public void Create_And_GetProduct()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var expected = new Product(productId)
            {
                Id = productId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            _productsController.Create(expected);

            _productIds.Add(productId);

            //Act
            var actual = _productsController.GetProduct(productId);
            
            //Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Delete()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId)
            {
                Id = productId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            _productsController.Create(product);

            _productIds.Add(productId);

            //Act
            _productsController.Delete(productId);

            //Assert
            _productsController.Invoking(controller => controller.GetProduct(productId))
                .ShouldThrow<HttpResponseException>();
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            var productId1 = Guid.NewGuid();
            var prouctId2 = Guid.NewGuid();
            var product1 = new Product(productId1)
            {
                Id = productId1,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            var product2 = new Product(prouctId2)
            {
                Id = prouctId2,
                Name = "Dummy Name 2",
                Description = "Dummy Description 2",
                Price = 543.21M,
                DeliveryPrice = 98.76M
            };
            _productsController.Create(product1);
            _productsController.Create(product2);

            _productIds.Add(productId1);
            _productIds.Add(prouctId2);
            
            //Act
            var actual = _productsController.GetAll();

            //Assert
            actual.Items.Should().Contain(product1);
            actual.Items.Should().Contain(product2);
        }

        [Test]
        public void SearchByName()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var expected = new Product(productId)
            {
                Id = productId,
                Name = productId.ToString(),
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            _productsController.Create(expected);

            _productIds.Add(productId);

            //Act
            var actual = _productsController.SearchByName(productId.ToString());

            //Assert
            actual.Items.Should().Contain(expected);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var product = new Product(productId)
            {
                Id = productId,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            _productsController.Create(product);

            var expected = product;
            expected.Name = "Dummy Name 1 updated";

            _productIds.Add(productId);

            //Act
            _productsController.Update(productId,expected);
            var actual = _productsController.GetProduct(productId);

            //Assert
            actual.Should().Be(expected);
        }
    }
}