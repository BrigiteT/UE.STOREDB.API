using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;
using UE.STOREDB.DOMAIN.Infrastructure.Repositories;

namespace UE.STOREDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _favoriteRepository.GetAll();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _favoriteRepository.GetById(id);
            if(user == null) { return NotFound();}
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Favorite favorite)
        {
            var result = await _favoriteRepository.Insert(favorite);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Favorite favorite)
        {
            if(id !=favorite.Id) { return NotFound(); }
            var result = await _favoriteRepository.Update(favorite);
            if (!result) {  return BadRequest(); }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _favoriteRepository.Delete(id);
            if(result == null) { return BadRequest(); }
            return Ok(result);
        }

    }
}
