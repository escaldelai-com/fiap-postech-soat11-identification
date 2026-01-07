using Restaurant.Identification.Application;
using Restaurant.Identification.Data;
using Restaurant.Identification.Facade;
using Restaurant.Identification.Presenter;
using Restaurant.Identification.WebApi;

var builder = WebApplication.CreateBuilder(args);
var version = new Version(1, 0, 0);

Console.WriteLine("Starting Restaurant Identification Web Api");
Console.WriteLine($"Version: {version}");
Console.WriteLine();

// Add services to the container.
builder.Services.AddControllers();
builder.Services
    .AddSingleton(version)
    .AddPresenter()
    .AddData()
    .AddApplication()
    .AddFacade()
    .AddAuthentication(builder.Configuration)
    .AddRestaurantAuthorization()
    .AddOpenApi();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandler();
app.MapControllers();


// Run the application
app.Run();
