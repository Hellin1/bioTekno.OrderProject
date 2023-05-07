using AutoMapper;
using bioTekno.OrderProject.Business.DependencyResolvers.Microsoft;
using bioTekno.OrderProject.Business.Helpers;
using bioTekno.OrderProject.Business.Services;
using bioTekno.OrderProject.UI.Mappings.AutoMapper;
using bioTekno.OrderProject.UI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDependencies();

//builder.Services.AddDependencies();

var profiles = ProfileHelper.GetProfiles();
//profiles.Add(new UserCreateModelProfile());
profiles.Add(new CreateOrderRequestProfile());

var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});

var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<RabbitMQService>(new RabbitMQService("localhost", "myqueue"));


var app = builder.Build();
var listener = app.Services.GetService<RabbitMQService>();
int counter = 1;
listener.Listen(message =>
{
    Console.WriteLine($"Received message: {message} count: {counter++}");
    // send order mail
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
