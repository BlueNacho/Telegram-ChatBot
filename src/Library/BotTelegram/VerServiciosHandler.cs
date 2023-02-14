using Telegram.Bot.Types;
using Proyecto;
using System.Text;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "distancia".
    /// </summary>
    public class VerServiciosHandler : BaseHandler
    {

        /// <summary>
        ///  Este handler responde al comando de "Ver mis servicios".
        /// </summary>
        /// <param name="next"></param>
        public VerServiciosHandler(BaseHandler next)
            : base(next)
        {
            this.Keywords = new string[] { "Ver mis servicios", "/VerMisServicios", "VerMisServicios" };
        }

        /// <summary>
        /// El handler se encarga de chequear los servicios ofrecidos por un trabajador en específico. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {
            int usuarioid = (int)message.Chat.Id;
            var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == usuarioid);
            var trabajador = (Trabajador)usuario;
            if (Singleton<GestionUsuario>.Instance.EsTrbajadorID((int) message.Chat.Id))
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine($"Servicios ofrecidos por {message.Chat.FirstName}:");
                foreach (Servicio element in Singleton<CatalogoServicio>.Instance.ServiciosOfrecidos(trabajador))
                {
                    SB.AppendLine($"-ID: {element.ServicioID}, Nombre: {element.Nombre}, Categoria: {element.Categoria}, Precio: {element.Precio}, Descripción: {element.Descr}, Trabajador: {element.TrabajadorProveedor.Nombre} {element.TrabajadorProveedor.Apellido} \n");
                }
                response = SB.ToString();
            }
            else
            {
                response = "No tienes acceso a este comando";
            }
        }

    }
}