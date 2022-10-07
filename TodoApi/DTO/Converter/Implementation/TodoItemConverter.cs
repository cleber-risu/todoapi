using TodoApi.Model;

namespace TodoApi.DTO.Converter
{
   public class TodoItemConverter : IParser<TodoItemDTO, TodoItem>, IParser<TodoItem, TodoItemDTO>
   {
      // Parse - TodoItemDTO to TodoItem
      public TodoItem Parse(TodoItemDTO origin)
      {
         return new TodoItem
         {
            Id = origin.Id,
            Name = origin.Name,
            IsComplete = origin.IsComplete
         };
      }

      // Parse - TodoItem to TodoItemDTO
      public TodoItemDTO Parse(TodoItem origin)
      {
         return new TodoItemDTO
         {
            Id = origin.Id,
            Name = origin.Name,
            IsComplete = origin.IsComplete
         };
      }

      // Parse - List<TodoItemDTO> to List<TodoItem>
      public IEnumerable<TodoItem> Parse(IEnumerable<TodoItemDTO> origin)
      {
         return origin.Select(x => Parse(x)).ToList();
      }

      // Parse - List<TodoItem> to List<TodoItemDTO>
      public IEnumerable<TodoItemDTO> Parse(IEnumerable<TodoItem> origin)
      {
         return origin.Select(x => Parse(x)).ToList();
      }
   }
}
