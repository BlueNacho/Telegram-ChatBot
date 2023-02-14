using System;

namespace Proyecto
{
    /// <summary>
    ///  Clase administrador encargada de borrar servicios y crear categorías. 
    /// </summary>
    public class Administrador : Usuario
    {
        /// <summary>
        ///  Constructor de la clase Administrador 
        /// </summary>
        public Administrador(string username, int id)
        :base(username, id)
        {
        }
    }
}