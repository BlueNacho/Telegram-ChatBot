using Telegram.Bot.Types;
using Proyecto;
using System.Text;
namespace Ucu.Poo.TelegramBot
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando buscar oferta.
    /// </summary>
    public class BuscarOfertaHandler : BaseHandler
    {
        /// <summary>
        /// El estado del comando.
        /// </summary>
        public BuscarOfertaState State { get; private set; }


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BuscarOfertaHandler"/>.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public BuscarOfertaHandler(BaseHandler next)
            : base(new string[] { "/BuscarOfertas", "BuscarOfertas", "Buscar Ofertas" }, next)
        {
            //this.calculator = calculator;
            this.State = BuscarOfertaState.Start;
        }

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En el primer mensaje cuando
        /// <see cref="BuscarOfertaHandler.State"/> es <see cref="BuscarOfertaState.Start"/> usa
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. En
        /// caso contrario eso implica que los sucesivos mensajes son parámetros del comando y se procesan siempre.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
        protected override bool CanHandle(Message message)
        {
            if (this.State == BuscarOfertaState.Start)
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
            int usuarioid = (int)message.Chat.Id;
            var usuario = Singleton<GestionUsuario>.Instance.Usuarios.Find(u => u.ID == usuarioid);
            var empleador = (Empleador)usuario;
            if (State == BuscarOfertaState.Start)
            {
                this.State = BuscarOfertaState.EvaluarPrompt;
                response = $"Ingrese una opción: \n 1.Buscar sin filtro \n 2.Buscar por reputación y distancia \n 3.Buscar por categoria ";

            }
            else if (State == BuscarOfertaState.EvaluarPrompt && message.Text == "1")
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ofertas sin filtros:");
                foreach (Servicio element in Singleton<CatalogoServicio>.Instance.BuscarServicioSinFiltro())
                {
                    SB.AppendLine($"-ID: {element.ServicioID}, Nombre: {element.Nombre}, Categoria: {element.Categoria}, Precio: {element.Precio}, Descripción: {element.Descr}, Trabajador: {element.TrabajadorProveedor.Nombre} {element.TrabajadorProveedor.Apellido} \n");
                }
                response = SB.ToString();
            }
            else if (State == BuscarOfertaState.EvaluarPrompt && message.Text == "2")
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ofertas por reputación y distancia:");
                foreach (Servicio element in Singleton<CatalogoServicio>.Instance.BuscarServicioSinCategoria(empleador))
                {
                    SB.AppendLine($"-ID: {element.ServicioID}, Nombre: {element.Nombre}, Categoria: {element.Categoria}, Precio: {element.Precio}, Descripción: {element.Descr}, Trabajador: {element.TrabajadorProveedor.Nombre} {element.TrabajadorProveedor.Apellido} \n");
                }
                response = SB.ToString();
            }
            else if (State == BuscarOfertaState.EvaluarPrompt && message.Text == "3")
            {
                this.State=BuscarOfertaState.FiltroCategoriaPrompt;
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ingrese una de las siguientes categorias:");
                foreach (string element in Singleton<CatalogoCategoria>.Instance.ListaCategoria)
                {
                    SB.AppendLine($"{element}\n");
                }
                response = SB.ToString();
            }
            else if (State == BuscarOfertaState.FiltroCategoriaPrompt)
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("Ofertas por categoria:");
                foreach (Servicio element in Singleton<CatalogoServicio>.Instance.BuscarServicioPorCategoria(message.Text, empleador))
                {
                    SB.AppendLine($"-ID: {element.ServicioID}, Nombre: {element.Nombre}, Categoria: {element.Categoria}, Precio: {element.Precio}, Descripción: {element.Descr}, Trabajador: {element.TrabajadorProveedor.Nombre} {element.TrabajadorProveedor.Apellido} \n");
                }
                response = SB.ToString();
                InternalCancel();
            }
            else
            {
                response = "";
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = BuscarOfertaState.Start;
        }

        /// <summary>
        /// Estados de Buscar Oferta
        /// </summary>
        public enum BuscarOfertaState
        {
            ///Start
            Start,
            ///EvaluarPrompt
            EvaluarPrompt,
            ///FiltroCategoriaPrompt
            FiltroCategoriaPrompt
        }
    }
}