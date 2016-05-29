using System;
using System.Net;
using System.Web.Http;
using refactor_me.DataAccess;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductsController()
        {
            _productOptionRepository = new ProductOptionRepository();
            _productRepository = new ProductRepository(_productOptionRepository);
        }

        public ProductsController(IProductRepository productRepository, IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }

        [Route]
        [HttpGet]
        public Products GetAll()
        {
            return _productRepository.GetAll();
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            return _productRepository.Get(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            _productRepository.Create(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            var productToUpdate = _productRepository.Get(id);
            if(productToUpdate == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.DeliveryPrice = product.DeliveryPrice;
            
            _productRepository.Update(productToUpdate);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productRepository.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return _productOptionRepository.GetByProductId(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptionRepository.Get(id);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            _productOptionRepository.Create(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var optionToUpdate = _productOptionRepository.Get(id);
            if(optionToUpdate == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            optionToUpdate.Name = option.Name;
            optionToUpdate.Description = option.Description;

            _productOptionRepository.Update(optionToUpdate);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var optionToDelete = _productOptionRepository.Get(id);
            _productOptionRepository.Delete(optionToDelete.Id);
        }
    }
}
