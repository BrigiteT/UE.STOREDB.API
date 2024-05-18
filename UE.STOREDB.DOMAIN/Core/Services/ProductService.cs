using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.DTO;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;

namespace UE.STOREDB.DOMAIN.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var productsDTO = new List<ProductListDTO>();
            foreach (var product in products)
            {
                var productListDTO = new ProductListDTO();
                productListDTO.Id = product.Id;
                productListDTO.Description = product.Description;
                productListDTO.ImageUrl = product.ImageUrl;
                productListDTO.Stock = product.Stock;
                productListDTO.Price = product.Price;
                productListDTO.Discount = product.Discount;
                productListDTO.CategoryId = product.CategoryId;
                productsDTO.Add(productListDTO);
            }
            return productsDTO;
        }
        public async Task<ProductListDTO> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            var productsDTO = new ProductListDTO();
            productsDTO.Id = product.Id;
            productsDTO.Description = product.Description;
            return productsDTO;
        }

        public async Task<DetailProductProductDTO> GetByIdWtithDetailProduct(int id)
        {
            var product = await _productRepository.GetById(id);
            var detailProductProductDTO = new DetailProductProductDTO();
            detailProductProductDTO.Id = id;
            detailProductProductDTO.Description = product.Description;
            var productDetailListDTO = new List<ProductDetailListDTO>();
            foreach (var dpp in product.ProductDetail)
            {
                var detailProduct = new ProductDetailListDTO();
                detailProduct.Id = dpp.Id;
                detailProduct.ImageUrl = dpp.ImageUrl;
                productDetailListDTO.Add(detailProduct);
            }
            detailProductProductDTO.ProductDetail = productDetailListDTO;
            return detailProductProductDTO;

        }

        public async Task<bool> Create(ProductCreateDTO productCreateDTO)
        {
            var product = new Product();
            product.Id = productCreateDTO.Id;
            product.Description = productCreateDTO.Description;

            return await _productRepository.Insert(product);

        }


    }
}
