using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace Proyecto
{
    /// <summary>
    ///  Clase gestión usuario. 
    /// </summary>
    public class GestionUsuario
    {
        /// <summary>
        /// Lista de los usuarios comunes del sistema
        /// </summary>
        /// <returns></returns>
        public List<UsuarioComun> Usuarios = new List<UsuarioComun>();

        /// <summary>
        /// Lista de los administradores del sistema
        /// </summary>
        /// <returns></returns>
        public List<Administrador> Administradores = new List<Administrador>();

        /// <summary>
        /// Método para crear un objeto de tipo Trabajador y agregarlo a la lista de usuarios
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cedula"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="genero"></param>
        /// <param name="celular"></param>
        /// <param name="mail"></param>
        /// /// <param name="id"></param>
        public Trabajador CrearTrabajador(string username, int cedula, string nombre, string apellido, string genero, int celular, string mail, int id)
        {
            Trabajador t = new Trabajador(username, cedula, nombre, apellido, genero, celular, mail, id);
            Usuarios.Add(t);
            return t;
        }

        /// <summary>
        /// Crea una instancia de administrador
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Administrador CrearAdmin(string username, int id)
        {
            Administrador a = new Administrador(username, id);
            Administradores.Add(a);
            return a;
        }

        /// <summary>
        /// Crea un Empleador y lo agrega a la lista de usuarios
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cedula"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="genero"></param>
        /// <param name="celular"></param>
        /// <param name="mail"></param>
        /// <param name="ubicacion"></param>
        /// <param name="id"></param>
        public Empleador CrearEmpleador(string username, int cedula, string nombre, string apellido, string genero, int celular, string mail, string ubicacion, int id)
        {
            Empleador e = new Empleador(username, cedula, nombre, apellido, genero, celular, mail, ubicacion, id);
            Usuarios.Add(e);
            return e;
        }

        /// <summary>
        /// Método que notifica al usuario. Agrega el mensaje a la lista de notificaciones del usuario. 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="mensaje"></param>
        public void NotificarUsuario(UsuarioComun usuario, string mensaje)
        {
            usuario.Notificaciones.Add(mensaje);
        }

        /// <summary>
        /// Verifica si el objeto que contiene la id es de tipo Trabajador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EsTrbajadorID(int id)
        {
            if (Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == id) != null)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == id);

                if (usuario.GetType() == typeof(Trabajador))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si el objeto que contiene la id es de tipo Empleador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EsEmpleadorID(int id)
        {
            if (Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == id) != null)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == id);

                if (usuario.GetType() == typeof(Empleador))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si el objeto que contiene la id es de tipo Administrador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EsAdminID(int id)
        {
            var usuario = Singleton<GestionUsuario>.Instance.Administradores.Find(u => u.ID == id);

            if (usuario.GetType() == typeof(Administrador))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Guarda la lista de usuarios en una persistencia en formato JSon
        /// </summary>
        public void GuardarEnJson()
        {
            List<Empleador> empleadores = new List<Empleador>();
            foreach (UsuarioComun u in this.Usuarios)
            {
                if (u != null)
                {
                    if (EsEmpleadorID(u.ID))
                    {
                        empleadores.Add((Empleador)u);
                        string jsonStringEmpleadores = JsonSerializer.Serialize(empleadores);
                        string rutaArchivoEmpleadores = "../../Persistencia/PersistenciaEmpleador.json";
                        File.WriteAllText(rutaArchivoEmpleadores, jsonStringEmpleadores);
                    }
                }
            }

            List<Trabajador> trabajadores = new List<Trabajador>();
            foreach (UsuarioComun u in this.Usuarios)
            {
                if (u != null)
                {
                    if (EsTrbajadorID(u.ID))
                    {
                        trabajadores.Add((Trabajador)u);
                        string jsonStringTrabajadores = JsonSerializer.Serialize(trabajadores);
                        string rutaArchivoTrabajadores = "../../Persistencia/PersistenciaTrabajador.json";
                        File.WriteAllText(rutaArchivoTrabajadores, jsonStringTrabajadores);
                    }
                }
            }

            if (this.Administradores.Count() > 0)
            {
                string jsonStringAdmins = JsonSerializer.Serialize(this.Administradores);
                string rutaArchivoAdmins = "../../Persistencia/PersistenciaAdmin.json";
                File.WriteAllText(rutaArchivoAdmins, jsonStringAdmins);
            }

        }

        /// <summary>
        /// Carga a la lista de usuarios los datos que estan dentro del archivo JSon
        /// </summary>
        public void CargarDesdeJson()
        {
            string rutaArchivoEmpleadores = "../../Persistencia/PersistenciaEmpleador.json";
            string rutaArchivoTrabajadores = "../../Persistencia/PersistenciaTrabajador.json";
            string rutaArchivoAdmins = "../../Persistencia/PersistenciaAdmin.json";

            if (System.IO.File.ReadAllText("../../Persistencia/PersistenciaEmpleador.json").Length > 0)
            {
                List<Empleador> empleadores = JsonSerializer.Deserialize<List<Empleador>>(File.ReadAllText(rutaArchivoEmpleadores));
                if (empleadores != null)
                {
                    foreach (Empleador e in empleadores)
                    {
                        this.Usuarios.Add(e);
                    }
                }
            }

            if (System.IO.File.ReadAllText("../../Persistencia/PersistenciaTrabajador.json").Length > 0)
            {
                List<Trabajador> trabajadores = JsonSerializer.Deserialize<List<Trabajador>>(File.ReadAllText(rutaArchivoTrabajadores));
                if (trabajadores != null)
                {
                    foreach (Trabajador t in trabajadores)
                    {
                        this.Usuarios.Add(t);
                    }
                }
            }

            if (System.IO.File.ReadAllText("../../Persistencia/PersistenciaAdmin.json").Length > 0)
            {
                List<Administrador> admins = JsonSerializer.Deserialize<List<Administrador>>(File.ReadAllText(rutaArchivoAdmins));
                if (admins != null)
                {
                    this.Administradores = admins;
                }
            }
        }
    }
}