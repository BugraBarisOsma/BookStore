using BookStore.Common;
using BookStore.Context;

using BookStore.Middlewares;
using BookStore.Services;
using BookStore.UnitOfWork.Abstract;
using BookStore.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<ILoggerService,ConsoleLogger>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

app.Run();
