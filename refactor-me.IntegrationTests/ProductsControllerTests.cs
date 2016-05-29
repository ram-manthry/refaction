using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public void Create()
        {
            //Arrange
            var productId1 = Guid.NewGuid();
            var product1 = new Product(productId1)
            {
                Id = productId1,
                Name = "Dummy Name 1",
                Description = "Dummy Description 1",
                Price = 123.45M,
                DeliveryPrice = 67.89M
            };
            _productsController.Create(product1);

            _productIds.Add(productId1);

            //Act
            var actual = _productsController.GetProduct(productId1);
            
            //Assert
            actual.Should().Be(product1);
        }

        //[Test]
        //public void GetAll()
        //{
        //    //Arrange
        //    var productId1 = new Guid();
        //    var prouctId2 = new Guid();
        //    var product1 = new Product(productId1)
        //    {
        //        Id = productId1,
        //        Name = "Dummy Name 1",
        //        Description = "Dummy Description 1",
        //        Price = 123.45M,
        //        DeliveryPrice = 67.89M
        //    };
        //    var product2 = new Product(prouctId2)
        //    {
        //        Id = prouctId2,
        //        Name = "Dummy Name 2",
        //        Description = "Dummy Description 2",
        //        Price = 543.21M,
        //        DeliveryPrice = 98.76M
        //    };
        //    _productsController.Create(product1);
        //    _productsController.Create(product2);

        //    _productIds.Add(productId1);
        //    _productIds.Add(prouctId2);

        //    //Act
        //    var products = _productsController.GetAll();

        //    //Assert
        //    products.Items.Should().Contain(product1);
        //    products.Items.Should().Contain(product2);
        //}

        //[Test]
        //public void SearchByName()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void GetProduct()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void Update()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void Delete()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void GetOptions()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void GetOption()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void CreateOption()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void UpdateOption()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}

        //[Test]
        //public void DeleteOption()
        //{
        //    //Arrange

        //    //Act

        //    //Assert

        //}
    }
}