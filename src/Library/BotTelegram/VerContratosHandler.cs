using Telegram.Bot.Types;
using Proyecto;
using System.Text;
using System;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "distancia".
    /// </summary>
    public class VerContratosHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public VerContratosState State { get; private set; }

        /// <summary>
        /// Los datos que va obteniendo el comando en los diferentes estados.
        /// </summary>
        public VerContratosState Data { get; private set; } = new VerContratosState();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearCategoriaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public VerContratosHandler(BaseHandler next)
            : base(new string[] { "Ver contratos", "/Ver Contratos", "ver contratos", "/VerContratos" }, next)
        {
            //this.calculator = calculator;
            this.State = VerContratosState.Start;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="VerContratosHandler.State"/> es <see cref="VerContratosState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == VerContratosState.Start)
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
            if (State == VerContratosState.Start)
            {
                this.State = VerContratosState.EvaluarPrompt;
                response = $"Ingrese un número: \n 1. Ver contratos pendientes \n 2. Ver contratos en curso \n 3. Ver contratos finalizados \n 4. Ver todos";
            }
            else if (message.Text=="1" && State== VerContratosState.EvaluarPrompt)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);                
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Contratos pendientes:\n");
                //Console.WriteLine(Singleton<CatalogoContrato>.Instance.ContratosPendientes(usuario)[0].ContratoID);
                foreach (Contrato element in Singleton<CatalogoContrato>.Instance.ContratosPendientes(usuario))
                {
                    SB.AppendLine($"-ID: {element.ContratoID}, Nombre del servicio: {element.Servicio.Nombre}, Empleador {element.Partes["Empleador"].Nombre} {element.Partes["Empleador"].Apellido}, Trabajador {element.Partes["Trabajador"].Nombre} {element.Partes["Trabajador"].Apellido}\n");
                }
                response = SB.ToString();
            }
            else if (message.Text=="2" && State== VerContratosState.EvaluarPrompt)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);                
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Contratos en curso:\n");
                foreach (Contrato element in Singleton<CatalogoContrato>.Instance.ContratosEnCurso(usuario))
                {
                    SB.AppendLine($"-ID: {element.ContratoID}, Nombre del servicio: {element.Servicio.Nombre}, Empleador {element.Partes["Empleador"].Nombre} {element.Partes["Empleador"].Apellido}, Trabajador {element.Partes["Trabajador"].Nombre} {element.Partes["Trabajador"].Apellido}\n");
                }
                response = SB.ToString();
            }
            else if (message.Text=="3" && State== VerContratosState.EvaluarPrompt)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);                
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Contratos finalizados:\n");
                foreach (Contrato element in Singleton<CatalogoContrato>.Instance.ContratosFinalizados(usuario))
                {
                    SB.AppendLine($"-ID: {element.ContratoID}, Nombre del servicio: {element.Servicio.Nombre}, Empleador {element.Partes["Empleador"].Nombre} {element.Partes["Empleador"].Apellido}, Trabajador {element.Partes["Trabajador"].Nombre} {element.Partes["Trabajador"].Apellido}\n");
                }
                response = SB.ToString();
            }
            else if (message.Text=="4" && State== VerContratosState.EvaluarPrompt)
            {
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);               
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Todos los contratos:\n");
                foreach (Contrato element in Singleton<CatalogoContrato>.Instance.ListaContrato)
                {
                    SB.AppendLine($"-ID: {element.ContratoID}, Nombre del servicio: {element.Servicio.Nombre}, Empleador {element.Partes["Empleador"].Nombre} {element.Partes["Empleador"].Apellido}, Trabajador {element.Partes["Trabajador"].Nombre} {element.Partes["Trabajador"].Apellido}\n");
                }
                response = SB.ToString();
            }
            else
            {
                this.State = VerContratosState.Start;
                response = "comando no válido";
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = VerContratosState.Start;

            //this.Data = new DistanceData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando DistanceHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide la dirección de origen y pasa al
        /// siguiente estado.
        /// - FromAddressPrompt: Luego de pedir la dirección de origen. En este estado el comando pide la dirección de
        /// destino y pasa al siguiente estado.
        /// - ToAddressPrompt: Luego de pedir la dirección de destino. En este estado el comando calcula la distancia
        /// y vuelve al estado Start.
        /// </summary>
        public enum VerContratosState
        {
            ///Start
            Start,
            ///EvaluarPrompt
            EvaluarPrompt,
            ///AddTrabajadorPrompt
            AddTrabajadorPrompt,
            ///AddEmpleadorPrompt
            AddEmpleadorPrompt
        }
    }
}