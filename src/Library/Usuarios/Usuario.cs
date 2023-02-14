using System;

namespace Proyecto
{
    /// <summary>
    ///  Clase Usuario. 
    /// </summary>
    public abstract class Usuario
    {
        /// <summary>
        /// ID del usuario (se extrae del Chat ID de telegram)
        /// </summary>
        /// <value></value>
        public int ID { get; set; }

        /// <summary>
        ///  Cada usuario tiene un Username característico. 
        /// </summary>
        public string Username { get ; set; }
    
        /// <summary>
        ///  Constructor de la clase Usuario. 
        /// </summary>
        protected Usuario(string username, int id)
        {
             if (username == null || id == null )
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else if (username == "")
            {
                throw new ArgumentException ("Parametro vacio.");
            }
            else
            {
                this.ID = id;
                this.Username = username;
            }

        }
    }
}