namespace TodoApi.DTO
{
   public class Link
   {
      public string Rel { get; set; } = string.Empty;
      public string? Href { get; set; }
      public string Action { get; set; } = string.Empty;
      public string[]? Types { get; set; }
   }
}
