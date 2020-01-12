using DotnetTodo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetTodo.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodosController : Controller
    {
        private TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return Ok(_context.Todos);
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
                return NotFound("Todo not found");

            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<Todo> Create([FromBody] Todo todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Name) || string.IsNullOrWhiteSpace(todo.Description))
                return BadRequest("Name and Description Required");

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public ActionResult<Todo> Update([FromBody] Todo todo, int id)
        {
            var todoToUpdate = _context.Todos.Find(id);
            if (todoToUpdate == null)
                return NotFound("Todo not found");

            if (!string.IsNullOrWhiteSpace(todo.Name)) todoToUpdate.Name = todo.Name;
            if (!string.IsNullOrWhiteSpace(todo.Description)) todoToUpdate.Description = todo.Description;
            _context.Todos.Update(todoToUpdate);
            _context.SaveChanges();

            return Ok(todoToUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
                return NotFound("Todo not found");

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return Ok();
        }
    }
}
