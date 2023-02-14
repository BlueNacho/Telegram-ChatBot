using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;

namespace Library.Test
{
    /// <summary>
    /// Test de funcionalidades de calificacion  
    /// </summary>
    public class TestCalificar
    {
        /// <summary>
        /// SetUp de los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Singleton<CatalogoServicio>.resetForTesting();
        }
        /// <summary>
        /// 10)	Como trabajador, quiero poder calificar a un empleador; el empleador me tiene que calificar a mí también, 
        /// si no me califica en un mes, la calificación será neutral, para que de esa forma pueda definir la reputación de mi empleador.
        /// </summary>
        [Test]
        public void TestCalificarEmpleador()
        {
            Empleador Juan = new Empleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            Trabajador Juana = new Trabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            UtilidadesCalificacion.CalificarEmpleador(Juan,5,"Muy buen Jefe, me agradó ofrecerle un servicio por su trato.");
            
            // Calculo la calificación del empleador, esta me tiene que dar 5 porque es la unica calificación que tiene por el momento. 
            Assert.AreEqual(UtilidadesCalificacion.CalcularCalificacion(Juan) , 5);
        }
        /// <summary>
        /// 11)	Como empleador, quiero poder calificar a un trabajador; el trabajador me tiene que calificar a mí también, si no me califica en un mes, 
        /// la calificación será neutral, para que, de esa forma, pueda definir la reputación del trabajador.
        /// </summary>
        [Test]
        public void TestCalificarServicio()
        {
            Empleador Juan = new Empleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            Trabajador Juana = new Trabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            UtilidadesCalificacion.CalificarServicio(Singleton<CatalogoContrato>.Instance.ListaContrato[0].Servicio,3,"Demoró más tiempo del que ofrecía en la descripción del servicio.");
            
            // Calculo la calificación del trabajador, esta me tiene que dar e porque es la unica calificación que se le fue otorgada. 
            Assert.AreEqual(UtilidadesCalificacion.CalcularCalificacion(Singleton<CatalogoContrato>.Instance.ListaContrato[0].Servicio), 3);
        }

        /// <summary>
        /// 12)	Como trabajador, quiero poder saber la reputación de un empleador que me contacte para que,
        ///  de esa forma, poder decidir sobre su solicitud de contratación.
        /// </summary>
        [Test]
        public void VerCalificacionEmpleador()
        {
            Empleador Juan = new Empleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);

            // Lo califico dos veces para comprobar que calcula bien la calificación- 
            UtilidadesCalificacion.CalificarEmpleador(Juan,6,"Excelente empleador, recomiendo por su amabilidad.");
            UtilidadesCalificacion.CalificarEmpleador(Juan,4,"Buena experiencia, me trató bien, unicamente remarcaría que demoró en contestarme.");

            Trabajador Juana = new Trabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");

            //Compruebo de que realmente me calcula bien el promedio. 
            Assert.AreEqual(UtilidadesCalificacion.CalcularCalificacion(Juan) , 5);
            
        }
    }
}