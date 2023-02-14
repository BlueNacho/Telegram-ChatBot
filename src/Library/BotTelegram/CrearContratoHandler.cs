using Telegram.Bot.Types;
using Proyecto;
using System.Text;
using System;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility.
    /// </summary>
    public class CrearContratoHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public CrearContratoState State { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearCategoriaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CrearContratoHandler(BaseHandler next)
            : base(new string[] { "/CrearContrato", "Crear contrato", "Crear contrato","CrearContrato" }, next)
        {
            this.State = CrearContratoState.Start;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="CrearContratoHandler.State"/> es <see cref="CrearContratoState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == CrearContratoState.Start)
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
            if (State == CrearContratoState.Start)
            {
                StringBuilder SB = new StringBuilder();
                //SB.AppendLine("Si desea ver los servicios disponibles ingrese: /BuscarOfertas");
                SB.AppendLine("Ingrese el ID del servicio a contratar");
                response=SB.ToString();
                this.State=CrearContratoState.Contratar;
            }
            else if (State==CrearContratoState.Contratar)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);
                var servicio = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == Convert.ToInt32(message.Text)); 
                Singleton<CatalogoContrato>.Instance.CrearContrato((Empleador)usuario, (Servicio)servicio);
                response="se creó el contrato";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                InternalCancel();
            }
            else
            {
                InternalCancel();
                response = "comando no válido";
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = CrearContratoState.Start;
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando DistanceHandler.
        /// - Start: El estado inicial del comando. Pide que ingrese el id del servicio a contratar
        /// - Contratar: busca si existe y lo crea un contrato con el mismo, obteniendo las partes del contrato
        /// </summary>
        public enum CrearContratoState
        {
            ///Start
            Start,
            ///EvaluarPrompt
            Contratar
        }
    }
}