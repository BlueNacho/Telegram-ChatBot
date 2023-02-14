using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace Proyecto
{
    /// <summary>
    ///  Clase
    /// </summary>
    public class CatalogoContrato
    {
        
        /// <summary>
        /// Lista de los contratos existentes.
        /// </summary>
        public List<Contrato> ListaContrato = new List<Contrato>();

        /// <summary>
        /// Crea una instancia de tipo Contrato, pasandole el empleador y servicio que estarán asociados al mismo, para posteriormente
        /// agregarla a la lista de contratos pendientes del trabajador
        /// </summary>
        /// <param name="empleador"></param>
        /// <param name="servicio"></param>
        /// 
        /// ESTO ES CREATOR
        public void CrearContrato(Empleador empleador, Servicio servicio)
        {
            Contrato contrato = new Contrato(servicio, empleador);
            this.ListaContrato.Add(contrato);              
        }

        /// <summary>
        /// Ingresa un True si acpeta el contrato o de lo contrario un False
        /// </summary>
        /// <param name="contrato"></param>
        /// 
        /// 
        public void AceptarContrato(Contrato contrato)
        {
            contrato.Estado.EnCurso();
        }
        /// <summary>
        /// Metodo para finalizar un contrato
        /// </summary>
        /// <param name="contrato"></param>
        public void FinalizarContrato(Contrato contrato)
        {
            contrato.FechaFin = DateTime.Now.Date;
            contrato.Estado.Terminado();
        }
        
        /// <summary>
        /// Retorna una lista de los contratos pendientes asociados a determinado usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Contrato> ContratosPendientes(UsuarioComun usuario)
        {
            var resultado = this.ListaContrato
            .FindAll(c => c.Partes.ContainsValue(usuario)/* && c.Estado.EstadoActual == c.Estado.OpcionEstados[0]*/);
            return resultado;
        }

        /// <summary>
        /// Retorna una lista de los contratos en curso asociados a determinado usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// EXPERT
        public List<Contrato> ContratosEnCurso(UsuarioComun usuario)
        {
            List<Contrato> resultado = ListaContrato
            .FindAll(c => c.Partes.ContainsValue(usuario) && c.Estado.EstadoActual == c.Estado.OpcionEstados[1]);
            return resultado;
        }

        /// <summary>
        /// Retorna una lista de los contratos finalizados asociados a determinado usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Contrato> ContratosFinalizados(UsuarioComun usuario)
        {
            List<Contrato> resultado = ListaContrato
            .FindAll(c => c.Partes.ContainsValue(usuario) && c.Estado.EstadoActual == c.Estado.OpcionEstados[2]);
            return resultado;
        }

        /// <summary>
        /// Guarda la lista de contratos en una persistencia en formato JSon
        /// </summary>
        public void GuardarEnJson()
        {
            if(this.ListaContrato.Count() > 0)
            {
                string jsonString = JsonSerializer.Serialize(this.ListaContrato);
                string rutaArchivo = "../../Persistencia/PersistenciaContrato.json";
                File.WriteAllText(rutaArchivo, jsonString);
            }
        }

        /// <summary>
        /// Carga a la lista de contratos los datos que estan dentro del archivo JSon
        /// </summary>
        public void CargarDesdeJson()
        {
            string rutaArchivo = "../../Persistencia/PersistenciaContrato.json";
            List<Contrato> contratos = JsonSerializer.Deserialize<List<Contrato>>(File.ReadAllText(rutaArchivo));
            this.ListaContrato = contratos;
        }
    }
}
