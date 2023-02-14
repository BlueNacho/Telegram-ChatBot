using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;


namespace Library.Test
{
    /// <summary>
    /// Test de los métodos de la clase Catalogo Categoría. 
    /// </summary>
    public class UnitTestCatalogoCategoria
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
        /// Test agregar categoría. 
        /// </summary> 
        [Test]
        public void TestAgregarCategoria()
        {
            Administrador Adm = new Administrador("NachoAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecánica");

            // Como es la única categoría creada, chequeo si esa es la categoría que agregue. 

            Assert.AreEqual(Singleton<CatalogoCategoria>.Instance.ListaCategoria[0],"Mecánica");
        }

        /// <summary>
        /// Test existencia categoría. 
        /// </summary> 
        [Test]
        public void TestExistenciaCategoria()
        {
            Administrador Adm = new Administrador("NachoAdm",100);
            Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecánica");
            Assert.AreEqual(Singleton<CatalogoCategoria>.Instance.ExistenciaCategoria("Mecánica"),true);
            
    
        }
    }
}