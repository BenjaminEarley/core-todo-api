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
    public class ListsController : ControllerBase
    {
        private readonly ListsRepository _repository;

        public ListsController(ListsRepository service)
        {
            _repository = service;
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            return Ok(await _repository.Get());
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(string id)
        {
            var todoList = await _repository.Get(id);

            if (todoList == null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        // PUT: api/Lists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(string id, List todoList)
        {
            if (id != todoList.Id)
            {
                return BadRequest();
            }

            await _repository.Update(id, todoList);

            return NoContent();
        }

        // POST: api/Lists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<List>> PostList(List todoList)
        {
            await _repository.Create(todoList);

            return CreatedAtAction(nameof(GetList), new { id = todoList.Id }, todoList);
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List>> DeleteList(string id)
        {
            var todoList = await _repository.Get(id);
            if (todoList == null)
            {
                return NotFound();
            }

            await _repository.Remove(todoList);

            return NoContent();
        }
    }
}
