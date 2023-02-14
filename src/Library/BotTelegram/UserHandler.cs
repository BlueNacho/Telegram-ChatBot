using Telegram.Bot.Types;
using Proyecto;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "distancia".
    /// </summary>
    public class UserHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public UserState State { get; private set; }

        /// <summary>
        /// Este handler responde al comando "Ingresar".
        /// </summary>
        /// <param name="next"></param>
        public UserHandler(BaseHandler next)
            : base(new string[] { "Ingresar", "/Ingresar", "ingresar" }, next)
        {
            //this.calculator = calculator;
            this.State = UserState.Start;
        }

        /// <summary>
        /// El CanHandle se encarga de chequear si el handler es el encargado de responder al mensaje. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == UserState.Start)
            {
                return base.CanHandle(message);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado indicando que el mensaje no pudo se procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            if (State == UserState.Start)
            {
                // En el estado Start le pide la dirección de origen y pasa al estado FromAddressPrompt
                this.State = UserState.EvaluarPrompt;
                response = $"Hola {message.From.FirstName}. ¿Cómo desea ingresar?: \n Trabajador (1) \n Empleador (2)";
            }
            else if (message.Text=="1" && State==UserState.EvaluarPrompt)
            {                
                response = $"Ingrese los siguientes datos en el formato indicado: Cedula-Genero-Celular-Mail ";
                this.State=UserState.AddTrabajadorPrompt;
                //InternalCancel();
            }
            else if (message.Text=="2" && State==UserState.EvaluarPrompt)
            {    
                response = $"Ingrese los siguientes datos en el formato indicado: Cedula-Genero-Celular-Mail-Ubicacion ";
                this.State=UserState.AddEmpleadorPrompt;
            }
            else if (State== UserState.AddTrabajadorPrompt)
            {
                string[] cadena=message.Text.Split("-");
                var usuario = Singleton<GestionUsuario>.Instance.CrearTrabajador(message.From.Username,int.Parse(cadena[0]) ,message.From.FirstName, message.From.LastName, cadena[1], int.Parse(cadena[2]), cadena[3], (int)message.Chat.Id);
                response=$"Se ha agregado el usuario con éxito";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
            }
            else if (State== UserState.AddEmpleadorPrompt)
            {
                string[] cadena=message.Text.Split("-");
                var usuario = Singleton<GestionUsuario>.Instance.CrearEmpleador(message.From.Username,int.Parse(cadena[0]) ,message.From.FirstName, message.From.LastName, cadena[1], int.Parse(cadena[2]), cadena[3],cadena[4] , (int)message.Chat.Id);
                response=$"Se ha agregado el usuario con éxito";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
            }
            else
            {
                response = "comando no válido";
            }   
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = UserState.Start;
        }

        /// <summary>
        /// Posibles estados del Handler. 
        /// </summary>
        public enum UserState
        {
            /// Estado inicial. 
            Start,
            /// Evalua condiciones. 
            EvaluarPrompt,
            /// Estado que agrega trabajador.
            AddTrabajadorPrompt,
            /// Estado que agrega empleador. 
            AddEmpleadorPrompt
        }
    }
}