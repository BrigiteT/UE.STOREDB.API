using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UE.STOREDB.DOMAIN.Core.DTO;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;

namespace UE.STOREDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeProducts)
        {
            if (includeProducts)
                return Ok(await _productService.GetByIdWtithDetailProduct(id));
            else
                return Ok(await _productService.GetById(id));
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] ProductCreateDTO product)
        {
            var result = await _productService.Create(product);
            if (!result){ return BadRequest(); }
            return Ok(result);
        }




        //private readonly IProductRepository _productRepository;

        //public ProductController(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _productRepository.GetAll();
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result = await _productRepository.GetById(id);
        //    if (result == null) { return NotFound(); }
        //    return Ok(result);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Insert([FromBody] Product product)
        //{
        //    var result = _productRepository.Insert(product);    
        //    return Ok(result);
        //}

        //[HttpPost("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] Product product)
        //{
        //    if (product.Id != id) { return NotFound(); }
        //    var result = await _productRepository.Update(product);
        //    if (!result) { return BadRequest(); }
        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _productRepository.Delete(id);
        //    if ( !result) { return BadRequest(); }
        //    return Ok(result);  
        //}


    }
}
