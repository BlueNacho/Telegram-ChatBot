using Telegram.Bot.Types;
using Proyecto;
using System;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Menu".
    /// </summary>
    public class MenuPrincipalHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MenuPrincipalHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public MenuPrincipalHandler(BaseHandler next)
            : base(next)
        {
            this.Keywords= new string[] {"Menu", "menu", "/Menu"};
        }

        /// <summary>
        /// El Handler despliega todos los menu según el tipo de usuario que es. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {
            if (Singleton<GestionUsuario>.Instance.EsTrbajadorID((int) message.Chat.Id))
            {
                // En el estado Start le pide la dirección de origen y pasa al estado FromAddressPrompt
                response = $"**************MENÚ TRBAJADOR****************** \n Escriba una de las siguientes opciones: \n -/OfrecerServicio \n -/GestionarContratos \n -/VerContratos \n -/Calificar \n -/VerMisServicios \n -/VerCalificacion \n -/VerNotificaciones";

            }
            else if (Singleton<GestionUsuario>.Instance.EsEmpleadorID((int) message.Chat.Id))
            {
                // En el estado Start le pide la dirección de origen y pasa al estado FromAddressPrompt
                response = $"**************MENÚ EMPLEADOR****************** \n Escriba una de las siguientes opciones: \n -/BuscarOfertas \n -/GestionarContratos \n -/VerContratos \n -/Calificar \n -/VerCalificacion \n -/VerNotificaciones";

            }
            else if (Singleton<GestionUsuario>.Instance.EsAdminID((int) message.Chat.Id))
            {
                // En el estado Start le pide la dirección de origen y pasa al estado FromAddressPrompt
                response = $"**************MENÚ ADMINISTRADOR****************** \n Escriba una de las siguientes opciones: \n -/CrearCategoria \n -/EliminarServicio";
            }
            else
            {
                response = "Comando no válido.";
            }
        }
    }
}
