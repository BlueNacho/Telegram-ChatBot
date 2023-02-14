using Telegram.Bot.Types;
using Proyecto;
using System.Text;
using System;
namespace Ucu.Poo.TelegramBot

{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando borrar servicio.
    /// </summary>
    public class BorrarServicioHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public BorrarServicioState State { get; private set; }


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CrearCategoriaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public BorrarServicioHandler(BaseHandler next)
            : base(new string[] { "/EliminarServicio", "Eliminar servicio", "/Eliminar servicio", "Eliminar Servicio" }, next)
        {
            this.State = BorrarServicioState.Start;
        }

                /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="BorrarServicioHandler.State"/> es <see cref="BorrarServicioState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == BorrarServicioState.Start)
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
            if (State == BorrarServicioState.Start && Singleton<GestionUsuario>.Instance.EsAdminID((int) message.Chat.Id))
            {
                this.State = BorrarServicioState.Checking;

                StringBuilder SB = new StringBuilder();
                foreach(Servicio s in Singleton<CatalogoServicio>.Instance.ListaServicio)
                {
                    SB.AppendLine($"ID: {s.ServicioID}, Categoria: {s.Categoria}, Nombre: {s.Nombre}, Trabajador: {s.TrabajadorProveedor.Username}");
                }
                
                response = $"Lista de servicios: \n {SB.ToString()} \n Escriba la ID del servicio que desea dar de baja";
            }
            else if ((this.State == BorrarServicioState.Checking))
            {
                var servicio = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == Convert.ToInt32(message.Text));
                if (servicio != null) 
                {
                    Singleton<CatalogoServicio>.Instance.BorrarServicio(servicio);
                    response = "El servicio se ha borrado con éxito. Se le notificará al trabajador correspondiente.";
                    Singleton<GestionUsuario>.Instance.GuardarEnJson();
                    Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                    Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                    Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                    InternalCancel();
                }
                else
                {
                    this.State = BorrarServicioState.Start;
                    response = "La ID que ingresó no esta asociada a ningun servicio, pruebe nuevamente.";
                }
            }
            else
            {
                response = "No tiene acceso a este comando.";
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = BorrarServicioState.Start;
        }

        /// <summary>
        /// Estados de Borrar Servicio
        /// </summary>
        public enum BorrarServicioState
        {
            ///Start
            Start,
            ///Checking
            Checking
        }
    }
}