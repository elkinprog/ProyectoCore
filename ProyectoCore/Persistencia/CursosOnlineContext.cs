using System;
using Dominio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistencia
{

    public class CursosOnlineContext: IdentityDbContext<Usuarios>
    {

            protected readonly IConfiguration Configuration;

            public CursosOnlineContext(IConfiguration configuration)
            {
            Configuration = configuration;
            }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorID, ci.CursoID });
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }

            public DbSet<Comentario>? Comentario { get; set; }
            public DbSet<Curso>? Curso { get; set; }
            public DbSet<CursoInstructor>? CursoInstructor { get; set; }
            public DbSet<Precio>? Precio { get; set; }

    }
}
