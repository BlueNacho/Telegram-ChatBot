using System;
using System.Collections.Generic;

namespace Proyecto
{
    /// <summary>
    ///  Clase Trabajador implementa UsuarioComun e ICalificacion
    /// </summary>
    public class Trabajador : UsuarioComun
    {
        /// <summary>
        ///  Constructor de la clase Trabajador
        /// </summary>
        public Trabajador(string username, int cedula, string nombre, string apellido, string genero, int celular, string mail, int id)
            :base(username, cedula,  nombre,  apellido,  genero,  celular,  mail, id)
        {
        }
    }
}
