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
    public class VerCalificacionHandler : BaseHandler
    {

        /// <summary>
        /// Esta clase procesa el mensaje "Ver calificación".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public VerCalificacionHandler(BaseHandler next)
            : base(next)
        {
            this.Keywords = new string[] { "Ver Calificacion", "/Ver Calificacion", "/VerCalificacion" };
        }

        /// <summary>
        /// El handler se encarga de ver la calificación del empleador o del trabajador según los servicios realizados.
        /// Para calcular la calificación, chequea de que clase es el usuario que llamó al comando.
        /// En caso de no tener calificaciones, devuelve un mensaje de que el usuario no tiene calificaciones. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        protected override void InternalHandle(Message message, out string response)
        {

            var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == (int)message.Chat.Id);
            StringBuilder SB = new StringBuilder();
            if (Singleton<GestionUsuario>.Instance.EsTrbajadorID((int)message.Chat.Id))
            {
                SB.AppendLine("Calificacion de los servicios:");
                foreach (Servicio s in Singleton<CatalogoServicio>.Instance.ServiciosOfrecidos((Trabajador)usuario))
                {
                    SB.AppendLine($"ID: {s.ServicioID}, Nombre: {s.Nombre}, Calificacion: {UtilidadesCalificacion.CalcularCalificacion(s)}");
                }
                response = SB.ToString();

            }
            else if (Singleton<GestionUsuario>.Instance.EsEmpleadorID((int)message.Chat.Id))
            {
                response = $"Tu calificación es: {UtilidadesCalificacion.CalcularCalificacion((Empleador)usuario)}";
            }
            else
            {
                response = "Comando inválido.";
                //this.State = CalificacionTrabajadorState.Start;
            }
        }
    }
}