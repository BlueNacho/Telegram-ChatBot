namespace Proyecto 
{
    /// <summary>
    /// Clase Estado
    /// </summary>
    public class Estado
    {
        /// <summary>
        /// Array con los estados posibles en los que puede estar el objeto
        /// </summary>
        /// <value></value>
        public string[] OpcionEstados = {"Pendiente", "En curso", "Terminado"};

        /// <summary>
        /// El estado actual del objeto
        /// </summary>
        public string EstadoActual;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Estado()
        {
            this.EstadoActual = OpcionEstados[0];
        }

        /// <summary>
        /// Metodo para cambiar el estado a "Pendiente"
        /// </summary>
        public void Pendiente()
        {
            this.EstadoActual = OpcionEstados[0];
        }

        /// <summary>
        /// Metodo para cambiar el estado a "En curso"
        /// </summary>
        public void EnCurso()
        {
            this.EstadoActual = OpcionEstados[1];
        }

        /// <summary>
        /// Metodo para cambiar el estado a "Terminado"
        /// </summary>
        public void Terminado()
        {
            this.EstadoActual = OpcionEstados[2];
        }
    }
}