using Microsoft.EntityFrameworkCore;
using TodoApi.Model;
using TodoApi.Model.Context;

namespace TodoApi.Repository
{
   public class TodoItemRepository : ITodoItemRepository
   {
      private readonly TodoContext _context;

      public TodoItemRepository(TodoContext context)
      {
         _context = context;
      }

      // FindAll
      public async Task<IEnumerable<TodoItem>> FindAll()
      {
         return await _context.TodoItems.ToListAsync();
      }

      // FindById
      public async Task<TodoItem?> FindById(long id)
      {
         return await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
      }

      // CreateTodoItem
      public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
      {
         try
         {
            await _context.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
         }
         catch (Exception)
         {
            throw;
         }
      }

      // UpdateTodoItem
      public async Task UpdateTodoItem(TodoItem todoItem)
      {
         if (!Exists(todoItem.Id)) throw new Exception();

         try
         {
            var result = await _context.TodoItems.FirstAsync(x => x.Id.Equals(todoItem.Id));
            _context.Entry(result).CurrentValues.SetValues(todoItem);
            await _context.SaveChangesAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      // DeleteTodoItem
      public async Task DeleteTodoItem(long id)
      {
         var result = await FindById(id);
         if (result == null) throw new Exception();

         try
         {
            _context.TodoItems.Remove(result);
            await _context.SaveChangesAsync();
         }
         catch (Exception)
         {
            throw;
         }
      }

      // Exists
      public bool Exists(long id)
      {
         return _context.TodoItems.Any(x => x.Id == id);
      }
   }
}
