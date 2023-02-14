using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;


namespace Library.Test
{
    /// <summary>
    /// Test de servicios
    /// </summary>
    public class TestServicio
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
        /// 4)	Como trabajador, quiero poder hacer ofertas de servicios; mi oferta indicará en qué categoría quiero publicar,
        /// tendrá una descripción del servicio ofertado, y un precio para que, de esa forma, 
        /// mis ofertas sean ofrecidas a quienes quieren contratar servicios.
        /// </summary> 
        [Test]
        public void CrearServicio()
        {
            Administrador Adm = new Administrador("GermanAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecánica");
            var t1 = Singleton<GestionUsuario>.Instance.CrearTrabajador("Nacho", 12345678, "Ignacio", "Berra", "Masculino", 095264781, "nacho@yahoo.com", 8);
            var t2 = Singleton<GestionUsuario>.Instance.CrearTrabajador("Rober123", 12345678, "Roberto", "Berra", "Masculino", 096587412, "roberto@yahoo.com", 9);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecánica","Mecánica automotriz.","Arreglo y cuido de su auto como ninguna otra persona.",4500, t1,"Montevideo");

            // Cree este servicio de prueba unicamente para comprobar si el elemento que se agrega a la ListaServicios por parte del trabajador es realmente de tipo Servicio.
            // Cuando el trabajador utiliza el metodo OfrecerServicio se crea un servicio y lo añade a la lista de catalogo de servicios.
            Servicio serv = new Servicio("Mecánica","Reparaciones automotrices", "Cuidamos de su auto.", 690,t2, "Montevideo" );
            // Al conocer que el elemento prueba es de tipo Servicio, puedo chequear si son del mismo tipo. 

            Assert.AreEqual(Singleton<CatalogoServicio>.Instance.ListaServicio[0].GetType(),serv.GetType());
         
        }

    }
}