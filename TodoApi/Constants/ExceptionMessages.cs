namespace TodoApi.Constants
{
   public static class ExceptionMessages
   {
      public const string NotFound = "Id Not Found.";
      public const string Integrity = "Can't delete selected item because there are related items.";
      public const string NullArgument = "Entity Not Exists.";
      public const string Update = "There was an error trying to save the data!";
   }
}
