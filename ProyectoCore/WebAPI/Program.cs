
using Aplicacion.Cursos;
using Dominio;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistencia;
using System;
using System.Reflection;
using WebAPI.Midleware;
using Microsoft.AspNetCore.Identity;



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://example.com", "http://www.contoso.com");
                      });
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CursosOnlineContext>(options =>
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);



builder.Services.AddMediatR(typeof(Aplicacion.Cursos.GetCurso).Assembly);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PostCurso>();
builder.Services.AddValidatorsFromAssemblyContaining<PutCurso>();

builder.Services.ConfigureApplicationCookie(identityOptionsCookies =>
{
    identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});


var app = builder.Build();

app.UseMiddleware<ManejadorErroresMidleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<CursosOnlineContext>();
            context.Database.Migrate();
            }
        catch (Exception e)
        {
           var loggin = services.GetRequiredService<ILogger<Program>>();
           loggin.LogError(e, "Ocurrio un eror en la migración");
        }
    }
}

//app.UseHttpsRedirection();
//app.UseDeveloperExceptionPage();
//app.UseFileServer();

app.UseAuthorization();

app.MapControllers();

app.Run();















