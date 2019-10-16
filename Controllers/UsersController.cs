using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UsersController(UserRepository service)
        {
            _repository = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repository.Get());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var User = await _repository.Get(id);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            await _repository.Update(id, User);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            await _repository.Create(User);

            return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var User = await _repository.Get(id);
            if (User == null)
            {
                return NotFound();
            }

            await _repository.Remove(User);

            return NoContent();
        }
    }
}
