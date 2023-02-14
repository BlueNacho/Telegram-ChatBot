using Telegram.Bot.Types;
using System.Collections.Generic;
using System;
using System.Linq;
using Proyecto;
  

namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando para crear una categoria.
    /// </summary>
    public class CrearCategoriaHandler : BaseHandler
    {

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public CrearCategoriaState State { get; private set; }

        /// <summary>
        /// Esta clase procesa el mensaje "Crear categoria".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CrearCategoriaHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/CrearCategoria", "/Crear categoria", "Crear categoria", "Crear Categoria"};
            this.State = CrearCategoriaState.Start;
            
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="CrearCategoriaHandler.State"/> es <see cref="CrearCategoriaState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == CrearCategoriaState.Start)
            {
                return base.CanHandle(message);
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Procesa el mensaje o comando "Crear categoria".
        /// /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            if (this.State == CrearCategoriaState.Start && Singleton<GestionUsuario>.Instance.EsAdminID((int) message.Chat.Id))
            {
                this.State = CrearCategoriaState.Checking;
                response = "Ingrese el nombre de la categoria a crear";
            }
            else if ((this.State == CrearCategoriaState.Checking))
            {
                if(Singleton<CatalogoCategoria>.Instance.ExistenciaCategoria(message.Text))
                {
                    this.State = CrearCategoriaState.Start;
                    response = "La categoria que intenta crear ya existe, pruebe con otra";
                }
                else
                {
                    Singleton<CatalogoCategoria>.Instance.AgregarCategoria(message.Text);
                    response = "La categoria ha sido creada con éxito.";
                    this.State=CrearCategoriaState.Start;
                    Singleton<GestionUsuario>.Instance.GuardarEnJson();
                    Singleton<CatalogoContrato>.Instance.GuardarEnJson();
                    Singleton<CatalogoCategoria>.Instance.GuardarEnJson();
                    Singleton<CatalogoServicio>.Instance.GuardarEnJson();
                    }
            }
            else
            {
                InternalCancel();
                response = "No tiene acceso a este comando.";
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = CrearCategoriaState.Start;
        }

        /// <summary>
        /// Estados del handler
        /// </summary>
        public enum CrearCategoriaState
        {
            ///Start
            Start,
            ///Checking
            Checking    
        }
    }
}