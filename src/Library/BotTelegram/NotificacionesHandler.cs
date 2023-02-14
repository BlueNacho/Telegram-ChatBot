using Telegram.Bot.Types;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto;


namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando necesario para conocer las notificaciones del usuario.
    /// </summary>
    public class NotificacionesHandler : BaseHandler
    {

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public NotificacionesState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje "Ver notificaciones".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public NotificacionesHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "Ver notificaciones", "/VerNotificaciones", "/Ver Notificaciones" };
            this.State = NotificacionesState.Start;

        }

        /// <summary>
        ///  Procesa si puede responder a ese mensaje.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == NotificacionesState.Start)
            {
                return base.CanHandle(message);
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// El handler despliega las notificaciones del trabajador. En caso de no tener, le avisa al usuario que no tiene notificaciones.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {
            if (this.State == NotificacionesState.Start)
            {
                int usuarioid = (int)message.Chat.Id;
                var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == usuarioid);
                var trabajador = (Trabajador)usuario;
                if (usuario.Notificaciones.Count() > 0)
                {
                    StringBuilder SB = new StringBuilder();
                    SB.AppendLine("Notificaciones");
                    foreach (string notificaciones in trabajador.Notificaciones)
                    {
                        SB.AppendLine($"{notificaciones}\n");
                    }

                    this.State = NotificacionesState.Completed;
                    response = SB.ToString();
                }
                else
                {
                    response = "No tienes notificaciones.";
                    InternalCancel();
                }
            }
            else
            {
                this.State = NotificacionesState.Start;
                response = "Comando no válido.";
            }
        }


        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = NotificacionesState.Start;
        }

        /// <summary>
        /// Posibles estados del Handler.
        /// </summary>
        public enum NotificacionesState
        {
            /// Estado Inicial
            Start,
            /// Estado completado
            Completed

        }
    }
}