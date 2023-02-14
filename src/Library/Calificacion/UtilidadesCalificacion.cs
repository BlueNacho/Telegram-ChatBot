using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto 
{
    /// <summary>
    ///  Clase calificación. 
    /// </summary>
    public class UtilidadesCalificacion
    {
        /// <summary>
        /// Calcula la calificacion de los objetos de tipo ICalificable
        /// </summary>
        /// <param name="calificable"></param>
        /// <returns></returns>
        public static int CalcularCalificacion(ICalificable calificable)
        {
            int cantidadCalificaciones = calificable.Calificaciones.Count();
            int valoracionTotal = calificable.Calificaciones.Sum(c => c.Valoracion);
            int promedioCalificaciones = 0;
            try
            {
                promedioCalificaciones = valoracionTotal / cantidadCalificaciones;
            }
            catch(DivideByZeroException)
            {
                promedioCalificaciones = 0;
            }
            return promedioCalificaciones;
        }

        /// <summary>
        /// Metodo para calificar un Empleador
        /// </summary>
        /// <param name="empleador"></param>
        /// <param name="valoracion"></param>
        /// <param name="comentario"></param>
        public static void CalificarEmpleador(Empleador empleador, int valoracion, string comentario)
        {
            Calificacion calificacion = new Calificacion(valoracion, comentario);
            empleador.Calificaciones.Add(calificacion);
        }

        /// <summary>
        /// Metodo para calificar un Servicio
        /// </summary>
        /// <param name="servicio"></param>
        /// <param name="valoracion"></param>
        /// <param name="comentario"></param>
        public static void CalificarServicio(Servicio servicio, int valoracion, string comentario)
        {
            Calificacion calificacion = new Calificacion(valoracion, comentario);
            servicio.Calificaciones.Add(calificacion);
        }

        /// <summary>
        /// Despues de un periodo de un mes de finaliazado el contrato, se califica de forma neutra a ambas partes del contrato
        /// </summary>
        public static void CalificacionNeutra(Contrato contrato)
        {
            DateTime fechaHoy = DateTime.Now.Date;
            if(contrato.FechaFin != default(DateTime))
            {
                if ((fechaHoy.Month - contrato.FechaFin.Month) > 0)
                {
                    CalificarEmpleador((Empleador) contrato.Partes["Empleador"], 3, "Expiró el periodo de calificacion, se le asigno una calificación neutra.");
                    CalificarServicio(contrato.Servicio, 3, "Expiró el periodo de calificacion, se le asigno una calificación neutra.");
                }
            }
            
        }
    }
}