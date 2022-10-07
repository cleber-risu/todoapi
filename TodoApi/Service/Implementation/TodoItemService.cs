using TodoApi.Constants.Hypermedia;
using TodoApi.DTO;
using TodoApi.DTO.Converter;
using TodoApi.Repository;

namespace TodoApi.Service
{
   public class TodoItemService : ITodoItemService
   {
      private readonly ITodoItemRepository _repository;
      private readonly HttpContextExt _httpContext;
      private readonly TodoItemConverter _converter;

      public TodoItemService(ITodoItemRepository repository, HttpContextExt httpContextExt)
      {
         _repository = repository;
         _httpContext = httpContextExt;
         _converter = new TodoItemConverter();
      }

      // FindAll
      public async Task<IEnumerable<TodoItemDTO>> FindAll()
      {
         var list = await _repository.FindAll();
         var result = _converter.Parse(list);
         return result;
      }

      // FindById
      public async Task<TodoItemDTO?> FindById(long id)
      {
         var result = await _repository.FindById(id);
         if (result == null) return null;
         var dto = _converter.Parse(result);
         return dto;
      }

      // CreateTodoItem
      public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
      {
         var todoItem = _converter.Parse(todoItemDTO);
         todoItem = await _repository.CreateTodoItem(todoItem);
         var dto = _converter.Parse(todoItem);
         return GetHateoas(dto);
      }

      // UpdateTodoItem
      public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
      {
         var todoItem = _converter.Parse(todoItemDTO);
         await _repository.UpdateTodoItem(todoItem);
      }

      // DeleteTodoItem
      public async Task DeleteTodoItem(long id)
      {
         await _repository.DeleteTodoItem(id);
      }

      // Exists
      public bool Exists(long id)
      {
         return _repository.Exists(id);
      }

      // GetHateoas
      public TodoItemDTO GetHateoas(TodoItemDTO todoItem)
      {
         todoItem.Links = new List<Link>();
         todoItem.Links.Add(HandleLink(
            RelationType.self,
            nameof(TodoApi.Controllers.TodoItemsController.GetTodoItem),
            HttpActionVerb.GET,
            new string[] { ResponseTypeFormat.INT, ResponseTypeFormat.JSON, ResponseTypeFormat.XML },
            new { id = todoItem.Id }
         ));
         todoItem.Links.Add(HandleLink(
            RelationType.self,
            nameof(TodoApi.Controllers.TodoItemsController.PostTodoItem),
            HttpActionVerb.POST,
            new string[] { ResponseTypeFormat.JSON, ResponseTypeFormat.XML },
            null
         ));
         todoItem.Links.Add(HandleLink(
            RelationType.self,
            nameof(TodoApi.Controllers.TodoItemsController.PutTodoItem),
            HttpActionVerb.PUT,
            new string[] { ResponseTypeFormat.INT, ResponseTypeFormat.JSON, ResponseTypeFormat.XML },
            new { id = todoItem.Id }
         ));
         todoItem.Links.Add(HandleLink(
            RelationType.self,
            nameof(TodoApi.Controllers.TodoItemsController.DeleteTodoItem),
            HttpActionVerb.DELETE,
            new string[] { ResponseTypeFormat.INT },
            new { id = todoItem.Id }
         ));


         return todoItem;
      }

      private Link HandleLink(string rel, string method, string action, string[]? types, object? value)
      {
         return new Link()
         {
            Rel = rel,
            Href = _httpContext.CreateHyperlink(method, value),
            Action = action,
            Types = types
         };
      }
   }
}
