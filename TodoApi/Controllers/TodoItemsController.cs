using Microsoft.AspNetCore.Mvc;
using TodoApi.DTO;
using TodoApi.Filters;
using TodoApi.Service;

namespace TodoApi.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class TodoItemsController : ControllerBase
   {
      private readonly ITodoItemService _service;

      public TodoItemsController(ITodoItemService service)
      {
         _service = service;
      }

      // GET: api/todoitems
      [HttpGet]
      [ServiceFilter(typeof(HateoasFilter))]
      public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
      {
         var result = await _service.FindAll();
         return new List<TodoItemDTO>(result);
      }

      // GET: api/todoitems/{id}
      [HttpGet("{id}")]
      [ServiceFilter(typeof(HateoasFilter))]
      public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
      {
         var todoItem = await _service.FindById(id);

         if (todoItem == null) return NotFound();

         return todoItem;
      }

      // POST: api/todoitems
      [HttpPost(Name = "Create")]
      public async Task<ActionResult<TodoItemDTO>> PostTodoItem([FromBody] TodoItemDTO todoItemDTO)
      {
         var result = await _service.CreateTodoItem(todoItemDTO);
         return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id }, result);
      }

      // PUT: api/todoitems/{id}
      [HttpPut("{id}")]
      public async Task<IActionResult> PutTodoItem([FromBody] TodoItemDTO todoItemDTO, long id)
      {
         if (todoItemDTO.Id != id) return BadRequest();

         try
         {
            await _service.UpdateTodoItem(todoItemDTO);
         }
         catch (Exception)
         {
            throw;
         }

         return NoContent();
      }

      // DELETE: api/todoitems/{id}
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteTodoItem(long id)
      {
         if (!_service.Exists(id)) return NotFound();

         try
         {
            await _service.DeleteTodoItem(id);
         }
         catch (Exception)
         {
            throw;
         }

         return NoContent();
      }

   }
}
