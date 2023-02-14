using Telegram.Bot.Types;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto;


namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando calificar trabajador.
    /// </summary>
    public class CalificarTrabajadorHandler : BaseHandler
    {

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public CalificarTrabajadorState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje "Calificar trabajador".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CalificarTrabajadorHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "Calificar", "/Calificar" };
            this.State = CalificarTrabajadorState.Start;

        }

        /// <summary>
        /// Procesa todos los mensajes y retorna true siempre.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {
            var id = message.Chat.Id;
            var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)id);
            if (this.State == CalificarTrabajadorState.Start)
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Contratos finalizados para calificar:\n");
                foreach (Contrato element in Singleton<CatalogoContrato>.Instance.ContratosFinalizados(usuario))
                {
                    SB.AppendLine($"-ID Contrato: {element.ContratoID}, Servicio: {element.Servicio.Descr} \n");
                }
                SB.AppendLine("Ingrese la información en el siguiente formato: ID contrato-valoracion-comentario");
                response = SB.ToString();
                this.State=CalificarTrabajadorState.SeleccionarContrato;
            }
            else if (this.State == CalificarTrabajadorState.SeleccionarContrato &&Singleton<GestionUsuario>.Instance.EsTrbajadorID((int)message.Chat.Id))
            {
                string[] cadena=message.Text.Split("-");
                var contrato = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.ContratoID == Convert.ToInt32(cadena[0]));
                UtilidadesCalificacion.CalificarEmpleador((Empleador)contrato.Partes["Empleador"],Convert.ToInt32(cadena[1]), cadena[2]);
                response="Calificado";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                InternalCancel();
            }
            else if (this.State == CalificarTrabajadorState.SeleccionarContrato &&Singleton<GestionUsuario>.Instance.EsEmpleadorID((int)message.Chat.Id))
            {
                string[] cadena=message.Text.Split("-");
                var contrato = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.ContratoID == Convert.ToInt32(cadena[0]));
                UtilidadesCalificacion.CalificarServicio(contrato.Servicio,Convert.ToInt32(cadena[1]), cadena[2]);
                response="Calificado";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                InternalCancel();
            }
            else
            {
                InternalCancel();
                response = "Debes ser empleador para poder calificar un servicio.";
            }
        }


        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = CalificarTrabajadorState.Start;
        }

        /// <summary>
        /// Chequea que este handler pueda responder a este mensaje.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == CalificarTrabajadorState.Start)
            {
                return base.CanHandle(message);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Estados de Calificar Trabajador
        /// </summary>
        public enum CalificarTrabajadorState
        {
            ///Start
            Start,
            ///SeleccionarContrato
            SeleccionarContrato

        }
    }
}