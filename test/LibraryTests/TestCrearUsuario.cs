using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;

namespace Library.Test
{
    /// <summary>
    /// Test usuarios
    /// </summary>
    public class TestCrearUsuario
    {
        /// <summary>
        /// SetUp de los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Singleton<GestionUsuario>.resetForTesting();
        }
        /// <summary>
        /// 3)	Como trabajador, quiero registrarme en la plataforma,
        ///  indicando mis datos personales e información de contacto para que, de esa forma, pueda proveer 
        /// información de contacto a quienes quieran contratar mis servicios.
        /// </summary>
        [Test]
        public void CrearTrabajador()
        {

            var t1 = Singleton<GestionUsuario>.Instance.CrearTrabajador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com",  2);
            var t2 = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "juana@gmail.com",  3);
            Assert.AreEqual(Singleton<GestionUsuario>.Instance.Usuarios.Count, 2);
        }
        /// <summary>
        /// 5)	Como empleador, quiero registrarme en la plataforma, indicando mis datos personales e información de contacto para que,
        ///  de esa forma, pueda proveer información de contacto a los trabajadores que quiero contratar.
        /// </summary>
        [Test]
        public void CrearEmpleador()
        {

            var e1 = Singleton<GestionUsuario>.Instance.CrearEmpleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "juancho@gmail.com", "Parque Batlle", 2);
            Assert.AreEqual(Singleton<GestionUsuario>.Instance.Usuarios.Count, 1);
        }
    }
}