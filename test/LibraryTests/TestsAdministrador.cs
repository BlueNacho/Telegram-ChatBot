using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;

namespace Library.Test
{
    /// <summary>
    /// Tests de administradores
    /// </summary>
    public class TestsAdministrador
    {
        /// <summary>
        /// SetUp de los tests.
        /// </summary>
        
        [SetUp]
        public void Setup()
        {
            Singleton<CatalogoCategoria>.resetForTesting();
            Singleton<GestionUsuario>.resetForTesting();
            Singleton<CatalogoServicio>.resetForTesting();
        }

        /// <summary>
        /// 1)	Cómo administrador, quiero poder indicar categorías sobre las cuales se realizarán las ofertas de servicios para que, 
        /// de esa forma, los trabajadores puedan clasificarlos.
        /// </summary>
        [Test]
        public void CrearCategorias()
        {
           Administrador Adm = new Administrador("NachoAdm",100);
           Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecánica");
           Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Jardinería");
           Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Construcción");
    
           List<string> ListaCategoriaPrueba = new List<string>();
           ListaCategoriaPrueba.Add("Mecánica");
           ListaCategoriaPrueba.Add("Jardinería");
           ListaCategoriaPrueba.Add("Construcción");
           
           Assert.AreEqual(Singleton<CatalogoCategoria>.Instance.ListaCategoria, ListaCategoriaPrueba);
         
        }
        /// <summary>
        /// 2)	Como administrador, quiero poder dar de baja ofertas de servicios, avisando al oferente para que, de esa forma,
        ///  pueda evitar ofertas inadecuadas.
        /// </summary>
        [Test]
        public void DarDeBajaServicio()
        {
            var t1 = Singleton<GestionUsuario>.Instance.CrearTrabajador("GonzaloFre",56938742,"Gonzalo","Freitas","Hombre",095687431,"gonzaloFreitas@yahoo.com",5);
            Administrador Adm = new Administrador("NachoAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Construcción");
            Singleton<CatalogoServicio>.Instance.OfrecerServicio("Construcción","Construcción casas","Construyo casas en tiempo record con precios tan bajos que son dificil de creer",1500, t1,"Montevideo");
            Singleton<CatalogoServicio>.Instance.BorrarServicio(Singleton<CatalogoServicio>.Instance.ListaServicio[0]);
            Assert.AreEqual(t1.Notificaciones[0], $"Se ha eliminado un servicio. Nombre del Servicio: Construcción casas");
        }
    }
}