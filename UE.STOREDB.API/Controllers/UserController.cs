using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Infrastructure.Repositories;

namespace UE.STOREDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userRepository.GetAll();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }

        [HttpPost]

        public async Task<IActionResult> Insert([FromBody] User user)
        {
            var result = _userRepository.Insert(user);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id) { return BadRequest(); }
            var result = await _userRepository.Update(user);
            if (!result) { return BadRequest(); }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(int id)
        {
            var result = await _userRepository.Delete(id);
            if (!result) { return BadRequest(); } 
            return Ok(result);
        }
    }
}
