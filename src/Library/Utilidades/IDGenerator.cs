using System.Collections.Generic;
using System.Linq;

namespace Proyecto
{
    /// <summary>
    /// Clase IDGenerator, encargada de generar ID´s.
    /// </summary>
    public class IDGenerator
    {
        /// <summary>
        /// Método para crear ID únicos para cada servicio. 
        /// </summary>
        /// <returns></returns>
        public int ServicioIDGenerator()
        {
            int ID = 1;
            if(Singleton<CatalogoServicio>.Instance.ListaServicio.Count > 0)
            {
                int lastID = Singleton<CatalogoServicio>.Instance.ListaServicio.Last().ServicioID;
                ID = lastID + 1;
            }
            
            return ID; 
        }

        /// <summary>
        /// Método para crear ID únicos para cada contrato. 
        /// </summary>
        /// <returns></returns>
        public int ContratoIDGenerator()
        {
            int ID = 1;
            if(Singleton<CatalogoContrato>.Instance.ListaContrato.Count > 0)
            {
                int lastID = Singleton<CatalogoContrato>.Instance.ListaContrato.Last().ContratoID;
                ID = lastID + 1;
            }
            
            return ID; 
        }

    }
}