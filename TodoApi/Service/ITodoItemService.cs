using TodoApi.DTO;

namespace TodoApi.Service
{
   public interface ITodoItemService
   {
      Task<IEnumerable<TodoItemDTO>> FindAll();
      Task<TodoItemDTO?> FindById(long id);
      Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItem);
      Task UpdateTodoItem(TodoItemDTO todoItem);
      Task DeleteTodoItem(long id);
      bool Exists(long id);
   }
}
