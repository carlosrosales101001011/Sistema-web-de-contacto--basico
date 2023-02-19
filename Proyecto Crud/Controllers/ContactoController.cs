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

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int? idContacto) //El signo de interrogacion es que va a aceptar valores nulos
        {
            if (idContacto == null)
            {
                return RedirectToAction("Inicio", "Contacto");
            }
            Contacto ocontacto = oLista.Where(c => c.IdContact == idContacto).FirstOrDefault();
            return View(ocontacto);
        }
        [HttpGet]
        public ActionResult ELiminar(int? idContacto) //El signo de interrogacion es que va a aceptar valores nulos
        {
            if (idContacto == null)
            {
                return RedirectToAction("Inicio", "Contacto");
            }
            Contacto ocontacto = oLista.Where(c => c.IdContact == idContacto).FirstOrDefault();
            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Eliminar(string IdContact)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarContacto", oconexion);
                cmd.Parameters.AddWithValue("IdContact", IdContact);

                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }
        [HttpPost]
        public ActionResult Editar(Contacto oContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_EditarContacto", oconexion);
                cmd.Parameters.AddWithValue("IdContact", oContacto.IdContact);
                cmd.Parameters.AddWithValue("Nombre", oContacto.Nombre);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);

                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }
        [HttpPost]
        public ActionResult Registrar(Contacto oContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Nombre", oContacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);

                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }

    }
}
