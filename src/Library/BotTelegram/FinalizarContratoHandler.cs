using Telegram.Bot.Types;
using Proyecto;
using System.Text;
using System;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "finalizar contrato".
    /// </summary>
    public class FinalizarContratoHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public FinalizarContratoState State { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearCategoriaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public FinalizarContratoHandler(BaseHandler next)
            : base(new string[] { "/FinalizarContrato", "Finalizar contrato", "Finalizar Contrato","FinalizarContrato" }, next)
        {
            this.State = FinalizarContratoState.Start;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="FinalizarContratoHandler.State"/> es <see cref="FinalizarContratoState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == FinalizarContratoState.Start)
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
            if (State == FinalizarContratoState.Start)
            {
                StringBuilder SB = new StringBuilder(); 
                SB.AppendLine("Ingrese el ID del contrato a finalizar");
                response=SB.ToString();
                this.State=FinalizarContratoState.Contratar;
            }
            else if (State==FinalizarContratoState.Contratar)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id); 
                var contrato = Singleton<CatalogoContrato>.Instance.ContratosPendientes(usuario).Find(s => s.ContratoID == Convert.ToInt32(message.Text)); 
                Singleton<CatalogoContrato>.Instance.FinalizarContrato(contrato);
                response="Se finalizó el contrato";
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
            this.State = FinalizarContratoState.Start;

            //this.Data = new DistanceData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando DistanceHandler.
        /// - Start: El estado inicial del comando. Le pise la ID del contrato a finalizar
        /// - Contratar: Busca el contrato para ser finalizado
        /// </summary>
        public enum FinalizarContratoState
        {
            ///Start
            Start,
            ///Contratar
            Contratar
        }
    }
}