namespace TodoApi.Model
{
   public class TodoItem
   {
      public long Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public bool IsComplete { get; set; }
      public string? Secret { get; set; }
   }
}
