using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{


    public class Usuarios: IdentityUser
    {
        public string? NombreCompleto { get; set; }

    }

}
