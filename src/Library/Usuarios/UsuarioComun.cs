using System.Collections.Generic;
using System;


namespace Proyecto
{
    /// <summary>
    ///  Clase UsuarioComun implementa la clase Usuario. 
    /// </summary>
    public abstract class UsuarioComun : Usuario
    {
        /// <summary>
        ///  Cédula del usuario. 
        /// </summary>
        public int Cedula {get;set;}
        /// <summary>
        ///  Nombre del usuario. 
        /// </summary>
        public string Nombre {get;set;}
        /// <summary>
        ///  Apellido del usuario. 
        /// </summary>
        public string Apellido {get;set;}
        /// <summary>
        ///  Género del usuario. 
        /// </summary>
        public string Genero {get;set;}
        /// <summary>
        ///  Celular del usuario.
        /// </summary>
        public int Celular {get;set;}
        /// <summary>
        ///  Mail del usuario. 
        /// </summary>
        public string Mail {get;set;}

        /// <summary>
        /// Lista de mensajes que le han enviado al usuario. Ejemplo de implementación es cuando le envian una notificación porque le borraron un 
        /// servicio que el estaba ofreciendo en el catálogo de servicios. 
        /// </summary>
        /// <value></value>
        public List<string> Notificaciones = new List<string>();

        /// <summary>
        ///  
        /// </summary>
        public UsuarioComun(string username, int cedula, string nombre, string apellido, string genero, int celular, string mail, int id)
            :base(username, id)
        {
            var largeCI = cedula.ToString().Length;

            if (username == null || cedula == null || nombre == null || apellido == null || genero == null || celular == null || mail == null || id == null)
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else if (username == "" || nombre == "" || apellido == "" || genero == ""|| mail == "")
            {
                throw new ArgumentException("Parametro vacio.");
            }
            else if (largeCI != 8)
            {
                throw new ExceptionCILarge("Largo de la cédula incorrecto.");
            }
            else
            {
                this.Cedula = cedula;
                this.Nombre = nombre;
                this.Apellido = apellido;
                this.Genero = genero;
                this.Celular = celular;
                this.Mail = mail; 
            }
        }

    }
}