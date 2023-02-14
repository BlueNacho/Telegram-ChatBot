using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;


namespace Library.Test
{
    /// <summary>
    /// Test de los 3 metodos de utilidades calificación.
    /// </summary>
    public class UnitTestClaseUtilidadesCalificacion
    {
        /// <summary>
        /// SetUp del test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Singleton<CatalogoCategoria>.resetForTesting();
            Singleton<GestionUsuario>.resetForTesting();
            Singleton<CatalogoServicio>.resetForTesting();
        }

        /// <summary>
        /// Ver calificación de un usuario empleador. 
        /// </summary> 
        [Test]
        public void VerCalificacionDeEmpleador()
        {
            Empleador Juan = new Empleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            // Como aún no ha sido calificado, creamos una excepción para que no de error y de que la calificación es 0.  
            Assert.AreEqual(UtilidadesCalificacion.CalcularCalificacion(Juan),0);
        }

        /// <summary>
        /// Calificar empleador. 
        /// </summary> 
        [Test]
        public void EmpleadorCalificado()
        {
            Empleador Juan = new Empleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            UtilidadesCalificacion.CalificarEmpleador(Juan,5,"Gran Jefe");
            //Como es la unica calificación, verifico que sea la que le pase. 
            Assert.AreEqual(UtilidadesCalificacion.CalcularCalificacion(Juan),5);
        }
    }
}