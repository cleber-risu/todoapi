using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoApi.Service;

namespace TodoApi.Filters
{
   public class HateoasFilter : IResultFilter
   {
      private readonly HateoasService _hateoasService;

      public HateoasFilter(HateoasService hateoasService)
      {
         _hateoasService = hateoasService;
      }

      public void OnResultExecuting(ResultExecutingContext context)
      {
         if (context.Result is not ObjectResult objectResult) return;

         var result = context.Result as ObjectResult;

         if (result?.StatusCode == StatusCodes.Status404NotFound) return;

         context.HttpContext.Response.ContentType = "application/hal+json";
         var response = _hateoasService.GenerateHateoasResponse(result?.Value);
         context.Result = new ObjectResult(response);
      }

      public void OnResultExecuted(ResultExecutedContext context) { }
   }
}
