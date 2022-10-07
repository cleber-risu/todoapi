namespace TodoApi.Model.Context
{
   public class SeedingService
   {
      private readonly TodoContext _context;

      public SeedingService(TodoContext context)
      {
         _context = context;
      }

      public void Seed()
      {
         if (_context.TodoItems.Any())
         {
            return;
         }

         TodoItem t1 = new TodoItem
         {
            Id = 1,
            Name = "Walk dog",
            IsComplete = true,
            Secret = "Is Secret"
         };

         TodoItem t2 = new TodoItem
         {
            Id = 2,
            Name = "Fat dog",
            IsComplete = false,
            Secret = "Is Secret"
         };

         TodoItem t3 = new TodoItem
         {
            Id = 3,
            Name = "Small dog",
            IsComplete = false,
            Secret = "Is Secret"
         };

         TodoItem t4 = new TodoItem
         {
            Id = 4,
            Name = "Evil cat",
            IsComplete = true,
            Secret = "Is Secret"
         };

         TodoItem t5 = new TodoItem
         {
            Id = 5,
            Name = "Good cat",
            IsComplete = true,
            Secret = "Is Secret"
         };

         TodoItem t6 = new TodoItem
         {
            Id = 6,
            Name = "Hungry hamster",
            IsComplete = false,
            Secret = "Is Secret"
         };

         _context.TodoItems.AddRange(t1, t2, t3, t4, t5, t6);

         _context.SaveChanges();
      }
   }
}
