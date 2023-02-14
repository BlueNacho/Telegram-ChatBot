using Telegram.Bot.Types;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto;


namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando para ofrecer servicio..
    /// </summary>
    public class OfrecerServicioHandler : BaseHandler
    {

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public ServicioState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje "Ofrecer servicio".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public OfrecerServicioHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "Ofrecer servicio", "/OfrecerServicio", "ofrecer servicio", "OfrecerServicio" };
            this.State = ServicioState.Start;

        }

        /// <summary>
        /// Determina si es responsabilidad de este handler el responder el mensaje. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == ServicioState.Start)
            {
                return base.CanHandle(message);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Este handler es el encargado de crear ofertas de servicio. Chequea los datos del servicio que ingresa el usuario y, en caso
        /// de que esten correctos, crea el servicio. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {
            if (this.State == ServicioState.Start)
            {
                this.State = ServicioState.Checking;
                response = "Indicanos las siguiente información sobre tu servicio en este formato: Categoría-Nombre-Descripción-Precio-Ubicación.";
            }
            else if (this.State == ServicioState.Checking)
            {
                string[] Datos = message.Text.Split("-");
                int usuarioid = (int)message.Chat.Id;
                var trabajador = (Trabajador)Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == usuarioid);

                Singleton<CatalogoServicio>.Instance.OfrecerServicio(Datos[0], Datos[1], Datos[2], Convert.ToDouble(Datos[3]), trabajador, Datos[4]);
                response = "Servicio creado";
                Singleton<GestionUsuario>.Instance.GuardarEnJson();
                Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                InternalCancel();
            }
            else
            {
                response = "No se ha podido registrar el servicio que deseaba. Vuelve a ingresar los datos para intentar crear un servicio de nuevo.";
                this.State = ServicioState.Start;
            }
        }


        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = ServicioState.Start;
        }

        /// <summary>
        /// Posibles estados del Handler.
        /// </summary>
        public enum ServicioState
        {
            /// Estado inicial. 
            Start,
            /// Estado de chequeo del mensaje que ingresó el usuario. 
            Checking,
            /// Estado de proceso completado. 
            Completed

        }
    }
}