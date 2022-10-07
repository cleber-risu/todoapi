using TodoApi.Constants.Hypermedia;
using TodoApi.DTO;

namespace TodoApi.Service
{
   public class HateoasService
   {
      private readonly HttpContextExt _httpContext;

      public HateoasService(HttpContextExt httpContext)
      {
         _httpContext = httpContext;
      }

      public object? GenerateHateoasResponse(object? value)
      {
         if (value is null) return null;

         if (value is TodoItemDTO)
         {
            var dto = value as TodoItemDTO;
            value = GetTodoItemHateoas(dto ?? new TodoItemDTO());
         }
         if (value is List<TodoItemDTO>)
         {
            var list = value as List<TodoItemDTO>;
            value = list?.Select(x => GetTodoItemHateoas(x)).ToList();
         }

         return value;
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

      public TodoItemDTO GetTodoItemHateoas(TodoItemDTO todoItem)
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
   }
}
