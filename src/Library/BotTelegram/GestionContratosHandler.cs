using Telegram.Bot.Types;
using Proyecto;
using System.Text;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "distancia".
    /// </summary>
    public class GestionContratosHandler : BaseHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public GestionContratosHandler(BaseHandler next)
            : base(next)
        {
            this.Keywords = new string[] { "/GestionarContratos", "Gestionar contratos", "GestionarContratos", "Gestionar Contratos" };
        }


        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            if (Singleton<GestionUsuario>.Instance.EsEmpleadorID((int)message.Chat.Id))
            {
                response = $"Ingrese la opción que desea hacer: \n-/CrearContrato \n -/FinalizarContrato \n";
            }
            else if (Singleton<GestionUsuario>.Instance.EsTrbajadorID((int)message.Chat.Id))
            {
                response = $"Ingrese la opción que desea hacer: \n-/AceptarContrato \n -/FinalizarContrato \n";
            }
            else{
                InternalCancel();
                response="Tu usuario no tiene acceso a este comando";
            }
        }

    }
}