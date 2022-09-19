
using Aplicacion.Cursos;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 
using Persistencia;
using System;
using System.Reflection;
using WebAPI.Midleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PostCurso>();
builder.Services.AddValidatorsFromAssemblyContaining<PutCurso>();


builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<CursosOnlineContext>(options => { 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

builder.Services.AddMediatR(typeof(Aplicacion.Cursos.GetCurso).Assembly);
builder.Services.AddMediatR(typeof(Aplicacion.Cursos.GetIdCurso).Assembly);
builder.Services.AddMediatR(typeof(Aplicacion.Cursos.PostCurso).Assembly);







var app = builder.Build();

app.UseMiddleware<ManejadorErroresMidleware>();  


if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();

    }

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();










