using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace Proyecto
{
    /// <summary>
    /// Clase CatalogoServicio encargada de agregar servicios y de eliminarlos de una lista de servicios.
    /// </summary>
    public class CatalogoServicio
    {
        /// <summary>
        /// Lista donde se guardan los servicios. 
        /// </summary>
        /// <returns></returns>
        public List<Servicio> ListaServicio = new List<Servicio>();

        /// <summary>
        /// Método para borrar servicios de la lista de servicios. 
        /// </summary>
        /// <param name="servicio"></param>
        /// <returns></returns>
        public void BorrarServicio(Servicio servicio)
        {
            this.ListaServicio.Remove(servicio);
            Notificaciones.Notificacion_BorrarServicio(servicio);
        }

        /// <summary>
        /// El método OfrecerServicio se le pasa por parametro todas las propiedades del Servicio, y adentro del método se crea
        /// una instancia de la clase Servicio y se agrega inmediatamente al catálogo de servicios. 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="nombreServicio"></param>
        /// <param name="descrServicio"></param>
        /// <param name="precioServicio"></param>
        /// <param name="trabajadorProveedor"></param>
        /// <param name="ubicacion"></param>
        public void OfrecerServicio(string categoria, string nombreServicio, string descrServicio, double precioServicio, Trabajador trabajadorProveedor, string ubicacion)
        {
            if (Singleton<CatalogoCategoria>.Instance.ExistenciaCategoria(categoria) == false)
            {
                throw new ExceptionCategoryExistence("Categoria inexistente");
            }
            else
            {
                Servicio servicio = new Servicio(categoria, nombreServicio, descrServicio, precioServicio, trabajadorProveedor, ubicacion);
                this.ListaServicio.Add(servicio);
            }
        }

        /// <summary>
        /// Devuelve los servicios ofrecidos por determinado trabajador
        /// </summary>
        /// <param name="trabajador"></param>
        /// <returns></returns>
        public List<Servicio> ServiciosOfrecidos(Trabajador trabajador)
        {
            var resultado = this.ListaServicio.FindAll(s => s.TrabajadorProveedor == trabajador);
            return resultado;
        }

        /// <summary>
        /// Filtra la lista de servicios por reputacion y ubicacion
        /// </summary>
        /// <param name="empleador"></param>
        /// <returns></returns>
        public List<Servicio> BuscarServicioSinCategoria(Empleador empleador)
        {
            IEnumerable<Servicio> listaOrdenada1 = this.ListaServicio
            .OrderBy(servicio => UtilidadesCalificacion.CalcularCalificacion(servicio)).Reverse();

            IEnumerable<Servicio> listaOrdenada2 = listaOrdenada1
            .OrderBy(servicio => CalculadorDistancia.CalcularDistancia(empleador, servicio));

            List<Servicio> resultado = listaOrdenada2.ToList();

            return resultado;
        }

        /// <summary>
        /// Filtra servicios por categoria, reputacion y distancia al empleador.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="empleador"></param>
        /// <returns></returns>
        public List<Servicio> BuscarServicioPorCategoria(string categoria, Empleador empleador)
        {
            List<Servicio> listaCategoria = BuscarServicioSinCategoria(empleador);
            return listaCategoria.FindAll(s => s.Categoria == categoria);
        }

        /// <summary>
        /// Devuelve toda la lista de servicios sin ordenar
        /// </summary>
        /// <returns></returns>
        public List<Servicio> BuscarServicioSinFiltro()
        {
            return this.ListaServicio;
        }

        /// <summary>
        /// Guarda la lista de servicios en una persistencia en formato JSon
        /// </summary>
        public void GuardarEnJson()
        {
            if(this.ListaServicio.Count() > 0)
            {
                string jsonString = JsonSerializer.Serialize(this.ListaServicio);
                string rutaArchivo = "../../Persistencia/PersistenciaServicio.json";
                File.WriteAllText(rutaArchivo, jsonString);
            }
        }

        /// <summary>
        /// Carga a la lista de servicios los datos que estan dentro del archivo JSon
        /// </summary>
        public void CargarDesdeJson()
        {
            string rutaArchivo = "../../Persistencia/PersistenciaServicio.json";
            List<Servicio> servicios = JsonSerializer.Deserialize<List<Servicio>>(File.ReadAllText(rutaArchivo));
            this.ListaServicio = servicios;
        }
    }
}