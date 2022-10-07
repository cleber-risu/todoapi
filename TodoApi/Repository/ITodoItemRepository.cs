using TodoApi.Model;

namespace TodoApi.Repository
{
   public interface ITodoItemRepository
   {
      Task<IEnumerable<TodoItem>> FindAll();
      Task<TodoItem?> FindById(long id);
      Task<TodoItem> CreateTodoItem(TodoItem todoItem);
      Task UpdateTodoItem(TodoItem todoItem);
      Task DeleteTodoItem(long id);
      bool Exists(long id);
   }
}
