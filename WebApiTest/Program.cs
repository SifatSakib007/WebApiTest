using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApiTest.Controllers;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryService, CategoryService>();

//Add services to the controller
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value?.Errors != null && e.Value.Errors.Count > 0)
                                      .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage): new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "validation failed"));
    };

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Api is working fine.");
app.MapControllers();

app.Run();


