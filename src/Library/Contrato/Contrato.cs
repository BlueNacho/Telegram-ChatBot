using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto
{
    /// <summary>
    ///  Cada contrato tiene un Servicio por el cual se creará un contrato. 
    /// </summary>
    public class Contrato
    {
        /// <summary>
        /// ID del contrato
        /// </summary>
        /// <value></value>
        public int ContratoID { get; set; }

        /// <summary>
        /// Cada contrato tiene un Servicio por el cual se creará un contrato. 
        /// </summary>
        public Servicio Servicio { get; set; }

        /// <summary>
        /// Diccionario que contiene las dos partes del contrato (dos usuarios comunes, empleador/trabajador)
        /// </summary>
        public Dictionary<string, UsuarioComun> Partes { get; set; }

        /// <summary>
        ///  Cada contrato tiene un empleador que se vinculará con el servicio a contratar en el constructor de clase Contrato. 
        /// </summary>
        public Estado Estado { get; set; }

        /// <summary>
        /// Fecha de inicio del contrato
        /// </summary>
        /// <value></value>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del contrato
        /// </summary>
        /// <value></value>
        public DateTime FechaFin { get; set; }

        /// <summary>
        ///  Constructor de la clase Contrato. Se le pasa como parametro un servicio y un empleador. El empleador es quien quiere el servicio y desea contratarlo. 
        /// </summary>
        public Contrato(Servicio servicio, Empleador empleador)
        {
            if (servicio == null || empleador == null)
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else
            {
                this.Partes = new Dictionary<string, UsuarioComun>()
                {
                    {"Empleador", empleador},
                    {"Trabajador", servicio.TrabajadorProveedor}
                };
                this.Servicio = servicio;
                this.Estado = new Estado();
                this.ContratoID = new IDGenerator().ContratoIDGenerator();
                this.FechaInicio = DateTime.Now;
                this.FechaFin = default(DateTime);
            }
        }


    }
}