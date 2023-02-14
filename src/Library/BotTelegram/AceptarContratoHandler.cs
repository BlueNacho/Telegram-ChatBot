using Telegram.Bot.Types;
using Proyecto;
using System.Text;
using System;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility.
    /// </summary>
    public class AceptarContratoHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public AceptarContratoState State { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearCategoriaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public AceptarContratoHandler(BaseHandler next)
            : base(new string[] { "/AceptarContrato", "Aceptar contrato", "Aceptar Contrato", "AceptarContrato" }, next)
        {
            this.State = AceptarContratoState.Start;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="AceptarContratoHandler.State"/> es <see cref="AceptarContratoState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == AceptarContratoState.Start)
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
            if (State == AceptarContratoState.Start)
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ingrese el ID del servicio que aceptará");
                response = SB.ToString();
                this.State = AceptarContratoState.Evaluar;
            }
            else if (State == AceptarContratoState.Evaluar)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ingrese 1 si aun desea aceptar el contrato, 2 si cancela.");
                foreach (Servicio s in Singleton<CatalogoServicio>.Instance.ServiciosOfrecidos((Trabajador)usuario))
                {
                    SB.AppendLine($"Servicios ID: {s.ServicioID}, Nombre: {s.Nombre}, Calificacion: {UtilidadesCalificacion.CalcularCalificacion(s)}");
                }
                response = SB.ToString();
                this.State = AceptarContratoState.Contratar;
            }
            else if (State == AceptarContratoState.Contratar)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);
                var contrato = Singleton<CatalogoContrato>.Instance.ContratosPendientes(usuario).Find(s => s.ContratoID == Convert.ToInt32(message.Text));
                if (message.Text == "1")
                {   
                    Singleton<CatalogoContrato>.Instance.AceptarContrato(contrato);
                    response = "Se aceptó el contrato";
                    Singleton<GestionUsuario>.Instance.GuardarEnJson();
                    Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                    Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                    Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                    InternalCancel();
                }else if (message.Text=="2")
                {
                    Singleton<CatalogoContrato>.Instance.FinalizarContrato(contrato);
                    //Para que no se active la caducacion de la calificacion
                    contrato.FechaFin = default(DateTime);
                    response="Contrato cancelado";
                    Singleton<GestionUsuario>.Instance.GuardarEnJson();
                    Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                    Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                    Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                }
                else{
                    response="Comando inavlido, intente de nuevo";
                    this.State=AceptarContratoState.Evaluar;
                }
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
            this.State = AceptarContratoState.Start;

            //this.Data = new DistanceData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando DistanceHandler.
        /// - Start: El estado inicial del comando. Pide que ingresen el ID del contrato que aceptará
        /// - Evaluar: muestra la reputación del usuario y le da la opcion de continuar o cancelar
        /// - Contratar: procesa la respuesta del estado anterior y acciona segun la misma, es decir que si continuó, se acepta el contrato, de lo contrario despliega un mensaje de cancelacion.
        /// </summary>
        public enum AceptarContratoState
        {
            ///Start
            Start,
            ///Evaluar
            Evaluar,
            ///EvaluarPrompt
            Contratar
        }
    }
}