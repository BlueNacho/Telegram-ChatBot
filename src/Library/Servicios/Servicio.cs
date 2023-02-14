using System.Collections.Generic;
using System.Collections;
using System;


namespace Proyecto 
{
    /// <summary>
    /// Clase Servicio
    /// </summary>
    public class Servicio : ICalificable, IDistanciable
    {
        /// <summary>
        /// Propiedad que indica la categoría del servicio. 
        /// </summary>
        /// <value></value>
        public string Categoria {get; set;}

        /// <summary>
        /// Propiedad que indica el nombre del servicio. 
        /// </summary>
        /// <value></value>
        public string Nombre {get; set;}
        /// <summary>
        /// Propiedad que describe el Servicio. 
        /// </summary>
        /// <value></value>
        public string Descr {get; set;}
        /// <summary>
        /// Propiedad que muestra el precio del servicio. 
        /// </summary>
        /// <value></value>
        public double Precio {get; set;}
        /// <summary>
        /// Necesitamos un trabajador de la clase Trabajador para unir servicio con quien lo ofrece. 
        /// </summary>
        /// <value></value>
        public Trabajador TrabajadorProveedor {get; set;}
        /// <summary>
        /// Cada servicio tiene un ID única para lograr identificar servicios dentro de los métodos en la lógica. 
        /// </summary>
        /// <value></value>
        public int ServicioID {get; set;}
        
        /// <summary>
        /// Propiedad Ubicacion será utilizada para conectar personas con servicios según la ubicación. 
        /// </summary>
        /// <value></value>
        public string Ubicacion {get; set;}
        /// <summary>
        /// Calificaciones del Servicio
        /// </summary>
        /// <value></value>
        public List<Calificacion> Calificaciones { get; set; }

        /// <summary>
        /// Constructor de la clase Servicio. 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="nombre"></param>
        /// <param name="descr"></param>
        /// <param name="precio"></param>
        /// <param name="trabajadorProveedor"></param>
        /// <param name="ubicacion"></param>
        public Servicio(string categoria, string nombre, string descr, double precio, Trabajador trabajadorProveedor, string ubicacion)
        {
            
            if (categoria == null || nombre == null || descr == null || precio == null || trabajadorProveedor == null || ubicacion == null)
            {
                throw new ArgumentNullException("Parametro nulo.");
            }
            else if (categoria == "" || nombre == "" || descr == "" || ubicacion == "")
            {
                throw new ArgumentException ("Parametro vacio.");
            }
            else
            {
                this.Categoria = categoria;
                this.Nombre = nombre;
                this.Descr = descr;
                this.Precio = precio;
                this.TrabajadorProveedor = trabajadorProveedor;
                this.Ubicacion = ubicacion;
                this.ServicioID = new IDGenerator().ServicioIDGenerator();
                this.Calificaciones = new List<Calificacion>();
            }
        }

    }
}