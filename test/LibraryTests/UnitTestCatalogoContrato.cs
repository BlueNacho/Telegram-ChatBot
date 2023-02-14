using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;


namespace Library.Test
{
    /// <summary>
    /// Test de los métodos de la clase Catálogo Contrato.
    /// </summary>
    public class UnitTestCatalogoContrato
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
        /// Test crear contrato. 
        /// </summary> 
        [Test]
        public void CrearContratoCatalogoContrato()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            Assert.AreEqual(Singleton<CatalogoContrato>.Instance.ContratosPendientes(Juan).Count, 1);
        }

        /// <summary>
        /// Test aceptar contrato. 
        /// </summary> 
        [Test]
        public void AceptarContratoCatalogoContrato()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            var contratoJuana = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.Partes["Trabajador"] == Juana);
            Singleton<CatalogoContrato>.Instance.AceptarContrato(contratoJuana);
            Estado prueba = new Estado();
            // Cuando creas un estado este se crea en pendiente automaticamente, como yo acepte el contrato, el estado del contrato aceptado
            // debe ser diferente al estado que inicialize para probar. 
            Assert.AreNotEqual(contratoJuana.Estado, prueba);

        }

        /// <summary>
        /// Test finalizar contrato.
        /// </summary> 
        [Test]
        public void FinalizarContrato()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            var contratoJuana = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.Partes["Trabajador"] == Juana);
            Singleton<CatalogoContrato>.Instance.AceptarContrato(contratoJuana);
            Singleton<CatalogoContrato>.Instance.FinalizarContrato(contratoJuana);
            Assert.AreEqual(Singleton<CatalogoContrato>.Instance.ContratosFinalizados(Juana).Count, 1);
        }

        /// <summary>
        ///  Chequear que hay contratos finalizados.
        /// </summary> 
        [Test]
        public void ContratosPendientes()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            var contratoJuana = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.Partes["Trabajador"] == Juana);
            Assert.AreEqual(Singleton<CatalogoContrato>.Instance.ContratosPendientes(Juana).Count, 1);
        }

        /// <summary>
        /// Chequear que hay contratos en curso.
        /// </summary> 
        [Test]
        public void ContratosEnCurso()
        {
            var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("Juancho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com", 3);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Cortar pasto", "Corto cesped en menos de 20 minutos", 1000, Juana, "Pocitos");
            Singleton<CatalogoContrato>.Instance.CrearContrato(Juan, Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            var contratoJuana = Singleton<CatalogoContrato>.Instance.ListaContrato.Find(c => c.Partes["Trabajador"] == Juana);
            Singleton<CatalogoContrato>.Instance.AceptarContrato(contratoJuana);
            Assert.AreEqual(Singleton<CatalogoContrato>.Instance.ContratosEnCurso(Juana).Count, 1);
        }
    }
}