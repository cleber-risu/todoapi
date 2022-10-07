using Microsoft.EntityFrameworkCore;
using TodoApi;
using TodoApi.Filters;
using TodoApi.Model.Context;
using TodoApi.Repository;
using TodoApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/* builder.Services.AddControllers(options =>
{
   options.Filters.Add<HateoasFilter>();
}); */

builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoApi"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextExt>();

builder.Services.AddScoped<HateoasFilter>();
builder.Services.AddScoped<HateoasService>();

builder.Services.AddTransient<SeedingService>(); // apenas para desenvolvimento apagar depois

builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


var app = builder.Build();

/* inicio seeding - usar apenas em desenvolvimento, apagar depois */
// dotnet run seeddata
if (args.Length == 1 && args[0].ToLower() == "seeddata")
   SeedData(app);

void SeedData(IHost app)
{
   var scoopedFactory = app.Services.GetService<IServiceScopeFactory>();

   using (var scope = scoopedFactory.CreateScope())
   {
      var service = scope.ServiceProvider.GetService<SeedingService>();
      service.Seed();
   }
}
/* fim seeding */

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   //app.UseSwagger();
   //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

