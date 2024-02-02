using DemoProject.Api.Exceptions;
using DemoProject.Application.Concrete;
using DemoProject.Application.Interface.Repository;
using DemoProject.Application.Interface.Service;
using DemoProject.Domain.Model;
using DemoProject.Infrastructure.DBContext;
using DemoProject.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IRepository<Book>, BookRepository>();
builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddDbContext<DemoProjectDbContext>(
        options => options.UseSqlServer("Server=LAPTOP-D396GR4H;Database=DemoProjectDb;user=sa;password=123456;TrustServerCertificate=True"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Book Catalog Api",
        Description = "An API which provides book catalog management data",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddLogging();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Catalog 1.0");
    }); ;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
