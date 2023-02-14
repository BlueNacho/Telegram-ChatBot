using Telegram.Bot.Types;
using Proyecto;

namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class HelloHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="HelloHandler"/>. Esta clase procesa el mensaje "hola".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public HelloHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"hola", "Hola"};
        }

        /// <summary>
        /// Procesa el mensaje "hola" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            bool loggedState = false;
            foreach(UsuarioComun u in Singleton<GestionUsuario>.Instance.Usuarios)
            {
                if(u.ID == (int) message.Chat.Id)
                {
                    loggedState = true;
                }
            }

            if(loggedState)
            {
                var usuarioActual = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int) message.Chat.Id);
                response = $"¡Hola de nuevo {usuarioActual.Username}! \n /Menu - Accede al menu";
            }
            else
            {
                response = $"¡Bienvenido al bot! \nA continuación te explicaremos los comandos para comunicarnos: \n-Primer paso \n /Ingresar -(¡Importante!) Ingresas al sistema como perfil de Trabajador o Empleador \n-Segundo paso: \n /Menu -Accedes al menu de tu perfil con sus respectivas funcionalidades \n-Tercer paso: \nSigue los pasos que te indica el bot, el cual es guiado en cada momento \n¡Gracias por tu atención!";
            }
            
        }
    }
}