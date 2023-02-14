using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace Proyecto
{
    /// <summary>
    /// Clase CatalogoCategoria encargada de administrar la lista de categoría, agregar una categoria en la misma y de chequear
    /// si existe o no una categoría.
    /// </summary>
    public class CatalogoCategoria
    {
        /// <summary>
        /// ListaCategoria almacena todas las categorías existentes.
        /// </summary>
        /// <returns></returns>
        public List<string> ListaCategoria = new List<string>();

        /// <summary>
        /// Método para agregar categorías. Unicamente la agregará en caso de que no exista
        /// una con el mismo nombre dentro de la lista de las categorías. 
        /// </summary>
        /// <param name="categoria"></param>
        public void AgregarCategoria(string categoria)
        {    
            this.ListaCategoria.Add(categoria);   
        }

        /// <summary>
        /// Método que comprueba la existencia de una categoría dentro de la lista de categorías. 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public bool ExistenciaCategoria(string categoria)
        {
            if(this.ListaCategoria.Contains(categoria))
            {
                return true;
            }
            else
            {
                return false;
                //throw new ExceptionCategoryExistence("Categoria inexistente");
            }
        }
        
        /// <summary>
        /// Guarda la lista de categorias en una persistencia en formato JSon
        /// </summary>
        public void GuardarEnJson()
        {
            if(this.ListaCategoria.Count() > 0)
            {
                string jsonString = JsonSerializer.Serialize(this.ListaCategoria);
                string rutaArchivo = "../../Persistencia/PersistenciaCategoria.json";
                File.WriteAllText(rutaArchivo, jsonString);
            }
        }

        /// <summary>
        /// Carga a la lista de categorias los datos que estan dentro del archivo JSon
        /// </summary>
        public void CargarDesdeJson()
        {
            string rutaArchivo = "../../Persistencia/PersistenciaCategoria.json";
            List<string> categorias = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(rutaArchivo));
            this.ListaCategoria = categorias;
        }
    }
}