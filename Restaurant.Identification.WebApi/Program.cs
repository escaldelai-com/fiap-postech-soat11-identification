using Restaurant.Identification.Application;
using Restaurant.Identification.Data;
using Restaurant.Identification.Facade;
using Restaurant.Identification.Presenter;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services
    .AddPresenter()
    .AddData()
    .AddApplication()
    .AddFacade()
    .AddOpenApi();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// Run the application
app.Run();
