using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;


namespace Library.Test
{
    /// <summary>
    /// Test de los métodos de la clase Catálogo Servicio.
    /// </summary>
    public class UnitTestCatalogoServicio
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
        /// Test Borrar Servicio y chequeo de servicios ofrecidos. 
        /// </summary> 
        [Test]
        public void TestBorrarServicio()
        {
            var t1 = Singleton<GestionUsuario>.Instance.CrearTrabajador("GonzaloFre",56938742,"Gonzalo","Freitas","Hombre",095687431,"gonzaloFreitas@yahoo.com",5);
            Administrador Adm = new Administrador("NachoAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Construcción");
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Construcción","Construcción casas","Construyo casas en tiempo record con precios tan bajos que son dificil de creer",1500, t1,"Montevideo");
            Singleton<CatalogoServicio>.Instance.BorrarServicio(Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            //Chequeo que la cantidad de servicios ofrecidos del trabajador sean 0 porque el único que tenía fue borrado. 
            Assert.AreEqual(Singleton<CatalogoServicio>.Instance.ServiciosOfrecidos(t1).Count,0);
        }

        /// <summary>
        /// Test Ofrecer Servicio y chequeo de servicios ofrecidos. 
        /// </summary> 
        [Test]
        public void TestOfrecerServicio()
        {
            Administrador Adm = new Administrador("GermanAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecánica");
            var t1 = Singleton<GestionUsuario>.Instance.CrearTrabajador("Nacho", 12345678, "Ignacio", "Berra", "Masculino", 095264781, "nacho@yahoo.com", 8);
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecánica","Mecánica automotriz.","Arreglo y cuido de su auto como ninguna otra persona.",4500, t1,"Montevideo");
            // Chequeo que el servicio se agregó correctamente a la lista de servicios. 
            Assert.AreEqual(Singleton<CatalogoServicio>.Instance.ServiciosOfrecidos(t1).Count,1);
        }
    }
}