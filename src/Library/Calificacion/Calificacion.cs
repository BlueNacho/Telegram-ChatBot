using System;
using System.Collections.Generic;

namespace Proyecto
{
    /// <summary>
    ///  Clase calificación. 
    /// </summary>
    public class Calificacion
    {
        /// <summary>
        ///  Propiedad comentario. Hace referencía a una descripción o review. 
        /// </summary>
        public string Comentario { get; set; }

        /// <summary>
        ///  Propiedad valoración. La valoración va desde 1 estrella a 5. 
        /// </summary>
        public int Valoracion { get; set; }

        /// <summary>
        ///  Constructor de la clase Calificación. Cada calificación (1-5 estrellas) va acompañada de un comentario. 
        /// </summary>
        public Calificacion(int valoracion, string comentario)
        {
            if (comentario == null || valoracion == null)
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else if (comentario == "")
            {
                throw new ArgumentException("Parametro vacio.");
            }
            else
            {
                this.Valoracion = valoracion;
                this.Comentario = comentario;
            }
        }
    }
}
