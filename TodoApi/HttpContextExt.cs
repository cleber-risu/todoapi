namespace TodoApi
{
   public class HttpContextExt
   {
      private readonly IHttpContextAccessor _httpAccessor;

      public HttpContextExt(IHttpContextAccessor httpAccessor)
      {
         _httpAccessor = httpAccessor;
      }

      public string? CreateHyperlink(string method, object? value)
      {
         var hyperlink = string.Empty;
         var httpContext = _httpAccessor.HttpContext;
         if (httpContext != null)
         {
            var link = httpContext.RequestServices.GetRequiredService<LinkGenerator>();
            hyperlink = link.GetUriByAction(
               httpContext,
               method,
               values: value
            );
         }
         return hyperlink;
      }

      public void SetCookie(string name, string value, CookieOptions options)
      {
         var httpContext = _httpAccessor.HttpContext;
         if (httpContext != null)
            httpContext.Response.Cookies.Append(name, value, options);
      }

      public string GetCookie(string name)
      {
         var httpContext = _httpAccessor.HttpContext;
         var value = string.Empty;
         if (httpContext != null)
            value = httpContext.Request?.Cookies[name];

         return value ?? string.Empty;
      }
   }
}
