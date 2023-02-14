using System.Collections.Generic;

namespace Proyecto
{
    /// <summary>
    ///  Interfaz publica ICalificacion
    /// </summary>
    public interface ICalificable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        List<Calificacion> Calificaciones { get; set; }
    }
}