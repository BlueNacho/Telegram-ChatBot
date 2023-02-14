using System;
using System.Collections.Generic;

namespace Proyecto
{
    /// <summary>
    ///  Clase Empleador implementa UsuarioComun e ICalificacion. 
    /// </summary>
    public class Empleador : UsuarioComun, ICalificable, IDistanciable
    {
        /// <summary>
        /// Calificaciones del empleador
        /// </summary>
        /// <value></value>
        public List<Calificacion> Calificaciones { get; set; }
        /// <summary>
        ///  Propiedad Ubicacion. El empleador tiene una ubicación determinada que será utilizada luego para filtrar servicios por proximidad. 
        /// </summary>
        public string Ubicacion{get;set;}
        /// <summary>
        ///  Constructor de la clase Empleador. 
        /// </summary>
        public Empleador(string username, int cedula, string nombre, string apellido, string genero, int celular, string mail, string ubicacion, int id)
            :base(username,  cedula,  nombre,  apellido,  genero, celular,  mail, id)
        {
            if(ubicacion == null)
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else if (ubicacion == "")
            {
                throw new ArgumentException("Parametro vacio.");
            }
            else
            {
                this.Ubicacion = ubicacion;
                this.Calificaciones = new List<Calificacion>();
            }
            
        }
    }
}
