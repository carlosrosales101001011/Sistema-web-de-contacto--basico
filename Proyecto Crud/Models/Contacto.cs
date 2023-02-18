using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Crud.Models
{
    public class Contacto
    {
        public int IdContact { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set;}
        public string Telefono { get; set; }
        public string Correo { get; set;}
    }
}