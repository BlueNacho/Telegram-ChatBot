namespace Proyecto
{
    /// <summary>
    /// Clase Notificaciones
    /// </summary>
    public class Notificaciones
    {
        /// <summary>
        /// El método Notificacion_BorrarServicio esta encargado de mandarle al Trabajador (es identificado mediante una propiedad ID del mismo)
        /// y le avisa que un servicio ha sido eliminado, para saber cual fue el eliminado, el trabajador recibe el ID del servicio. 
        /// </summary>
        /// <param name="servicio"></param>
        public static void Notificacion_BorrarServicio(Servicio servicio)
        {
            Singleton<GestionUsuario>.Instance.NotificarUsuario(servicio.TrabajadorProveedor, $"Se ha eliminado un servicio. Nombre del Servicio: {servicio.Nombre}");
        }
    }
}