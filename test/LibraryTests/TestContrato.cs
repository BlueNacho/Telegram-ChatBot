using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;

namespace Library.Test
{
    /// <summary>
    /// Test contrato
    /// </summary>
    public class TestContrato
    {
        /// <summary>
        /// SetUp de los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Singleton<GestionUsuario>.resetForTesting();
            Singleton<CatalogoServicio>.resetForTesting();
            Singleton<CatalogoContrato>.resetForTesting();
        }
        /// <summary>
        /// 4)	Como trabajador, quiero poder hacer ofertas de servicios; mi oferta indicará en qué categoría quiero publicar, tendrá una descripción del servicio ofertado,
        ///  y un precio para que, de esa forma, mis ofertas sean ofrecidas a quienes quieren contratar servicios.
        /// </summary>
        [Test]
        public void CrearContrato()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);

            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);

            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");

            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);

            Assert.AreEqual(Singleton<CatalogoContrato>.Instance.ContratosPendientes(Juan).Count, 1);
        }
        /// <summary>
        /// 9)	Como empleador, quiero poder contactar a un trabajador para que de esa forma pueda, contratar una oferta de servicios determinada.
        /// </summary>
        [Test]
        public void TestAceptarContrato()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);

            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);

            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");

            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);

            var contratoJuana = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.Partes["Trabajador"] == Juana);

            Singleton<CatalogoContrato>.Instance.AceptarContrato(contratoJuana);

            Assert.AreEqual(contratoJuana.Estado.EstadoActual, "En curso");
        }
    }
}