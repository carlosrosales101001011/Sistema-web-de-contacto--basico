using Proyecto_Crud.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Crud.Controllers
{
    public class ContactoController : Controller
    {
        //Me va a permitir acceder a la hoja de webConfig, y luego vas a leer un elemento que se llama cadena
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        //
        private static List<Contacto> oLista = new List<Contacto>();





        // GET: Contacto
        public ActionResult Inicio()
        {
            oLista = new List<Contacto>();
            using(SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", oconexion);

                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                //Permite leer todo los resultados que estan mostrando nuestro comando
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();
                        nuevoContacto.IdContact = Convert.ToInt32(dr["IdContact"]);
                        nuevoContacto.Nombre = dr["Nombre"].ToString();
                        nuevoContacto.Apellidos = dr["Apellidos"].ToString();
                        nuevoContacto.Telefono = dr["Telefono"].ToString();
                        nuevoContacto.Correo = dr["Correo"].ToString();

                        oLista.Add(nuevoContacto);
                    }
                }
            }
            return View(oLista);
        }

    }
}
