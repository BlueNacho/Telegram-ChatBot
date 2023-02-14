using Ucu.Poo.Locations.Client;

namespace Proyecto
{
    /// <summary>
    /// Clase para calcular distancias
    /// </summary>
    public class CalculadorDistancia
    {
        /// <summary>
        /// Calcula las distancias entre los parametros de ubicacion (string) de dos objetos IDistanciable
        /// </summary>
        /// <param name="distanciable1"></param>
        /// <param name="distanciable2"></param>
        /// <returns></returns>
        public static double CalcularDistancia(IDistanciable distanciable1, IDistanciable distanciable2)
        {
            LocationApiClient client = new LocationApiClient();
            Location location1 = client.GetLocation(distanciable1.Ubicacion);
            Location location2 = client.GetLocation(distanciable2.Ubicacion);
            Distance distancia = client.GetDistance(location1, location2);
            return distancia.TravelDistance;
        }
    }
}
