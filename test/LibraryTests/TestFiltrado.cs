using NUnit.Framework;
using System;
using Proyecto;
using System.Collections.Generic;
using System.Linq;

namespace Library.Test;

/// <summary>
/// Test filtrados de busqueda
/// </summary>
public class TestFiltrado
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
    /// 6)	Como empleador, quiero buscar ofertas de trabajo, opcionalmente filtrando por categoría para que, de esa forma, pueda contratar un servicio.
    /// 7)	Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma ascendente de distancia a mi ubicación, es decir,
    ///  las más cercanas primero para que de esa forma, pueda poder contratar un servicio.
    /// 8)	Como empleador, quiero ver el resultado de las búsquedas de ofertas de trabajo ordenado en forma descendente por reputación, es decir,
    ///  las de mejor reputación primero para que de esa forma, pueda contratar un servicio.
    /// </summary>
    
    /// <summary>
    /// Filtrado sin categoria
    /// </summary>
    [Test]
    public void FiltradoSinCategoria() 
    {
        Singleton<CatalogoCategoria>.resetForTesting();
        Singleton<GestionUsuario>.resetForTesting();
        Singleton<CatalogoServicio>.resetForTesting();

        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecanica");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Jardineria");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Fletes");

        var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "a@gmail.com", "Parque Batlle", 1);
        var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "b@gmail.com", 2);
        var Pedro = Singleton<GestionUsuario>.Instance.CrearTrabajador("Pedro", 12345678, "Pedro", "Ramirez", "Masculino", 096123456, "c@gmail.com", 3);
        var Nacho = Singleton<GestionUsuario>.Instance.CrearTrabajador("Nacho", 12345678, "Nacho", "Gonzales", "Masculino", 096123456, "d@gmail.com", 4);
        var Kevin = Singleton<GestionUsuario>.Instance.CrearTrabajador("Kevin", 12345678, "Kevin", "Gonzales", "Masculino", 096123456, "e@gmail.com", 5);
        var Raul = Singleton<GestionUsuario>.Instance.CrearTrabajador("Raul", 12345678, "Raul", "Rodriguez", "Masculino", 096123456, "f@gmail.com", 6);

        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 4000, Juana, "8 de octubre");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 3000, Pedro, "18 de julio");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Corta pasto", "Ejemplo", 1000, Juana, "Luis Alberto de Herrera");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Floreria", "Ejemplo", 1000, Nacho, "Plaza independencia");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes largos", "Ejemplo", 1500, Kevin, "Sayago");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes pesados", "Ejemplo", 1200, Raul, "Maroñas");

        Calificacion c1 = new Calificacion(3, "Gracias");
        var s1 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 1);
        s1.Calificaciones.Add(c1);

        Calificacion c2 = new Calificacion(4, "Gracias");
        var s2 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 2);
        s2.Calificaciones.Add(c2);

        Calificacion c3 = new Calificacion(3, "Gracias");
        var s3 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 3);
        s3.Calificaciones.Add(c3);

        Calificacion c4 = new Calificacion(2, "Gracias");
        var s4 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 4);
        s4.Calificaciones.Add(c4);

        Calificacion c5 = new Calificacion(4, "Gracias");
        var s5 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 5);
        s5.Calificaciones.Add(c5);

        Calificacion c6 = new Calificacion(2, "Gracias");
        var s6 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 6);
        s6.Calificaciones.Add(c6);

        List<Servicio> listaComparar = new List<Servicio>();
        listaComparar.Add(s1);
        listaComparar.Add(s3);
        listaComparar.Add(s2);
        listaComparar.Add(s6);
        listaComparar.Add(s4);
        listaComparar.Add(s5);

        Assert.AreEqual(Singleton<CatalogoServicio>.Instance.BuscarServicioSinCategoria(Juan), listaComparar);
    }
    
    /// <summary>
    /// Filtrado con categoria
    /// </summary>
    [Test]
    public void FiltradoCategoria() 
    {
        Singleton<CatalogoCategoria>.resetForTesting();
        Singleton<GestionUsuario>.resetForTesting();
        Singleton<CatalogoServicio>.resetForTesting();

        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecanica");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Jardineria");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Fletes");

        var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "a@gmail.com", "Parque Batlle", 1);
        var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "b@gmail.com", 2);
        var Pedro = Singleton<GestionUsuario>.Instance.CrearTrabajador("Pedro", 12345678, "Pedro", "Ramirez", "Masculino", 096123456, "c@gmail.com", 3);
        var Nacho = Singleton<GestionUsuario>.Instance.CrearTrabajador("Nacho", 12345678, "Nacho", "Gonzales", "Masculino", 096123456, "d@gmail.com", 4);
        var Kevin = Singleton<GestionUsuario>.Instance.CrearTrabajador("Kevin", 12345678, "Kevin", "Gonzales", "Masculino", 096123456, "e@gmail.com", 5);
        var Raul = Singleton<GestionUsuario>.Instance.CrearTrabajador("Raul", 12345678, "Raul", "Rodriguez", "Masculino", 096123456, "f@gmail.com", 6);

        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 4000, Juana, "8 de octubre");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 3000, Pedro, "18 de julio");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Corta pasto", "Ejemplo", 1000, Juana, "Luis Alberto de Herrera");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Floreria", "Ejemplo", 1000, Nacho, "Plaza independencia");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes largos", "Ejemplo", 1500, Kevin, "Sayago");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes pesados", "Ejemplo", 1200, Raul, "Maroñas");

        Calificacion c1 = new Calificacion(3, "Gracias");
        var s1 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 1);
        s1.Calificaciones.Add(c1);

        Calificacion c2 = new Calificacion(4, "Gracias");
        var s2 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 2);
        s2.Calificaciones.Add(c2);

        Calificacion c3 = new Calificacion(3, "Gracias");
        var s3 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 3);
        s3.Calificaciones.Add(c3);

        Calificacion c4 = new Calificacion(2, "Gracias");
        var s4 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 4);
        s4.Calificaciones.Add(c4);

        Calificacion c5 = new Calificacion(4, "Gracias");
        var s5 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 5);
        s5.Calificaciones.Add(c5);

        Calificacion c6 = new Calificacion(2, "Gracias");
        var s6 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 6);
        s6.Calificaciones.Add(c6);

        List<Servicio> listaComparar = new List<Servicio>();
        listaComparar.Add(s6);
        listaComparar.Add(s5);

        Assert.AreEqual(Singleton<CatalogoServicio>.Instance.BuscarServicioPorCategoria("Fletes", Juan), listaComparar);
    }

    /// <summary>
    /// Devuelve una lista de todos los servicios en el sistema
    /// </summary>
    [Test]
    public void SinFiltros()
    {
        Singleton<CatalogoCategoria>.resetForTesting();
        Singleton<GestionUsuario>.resetForTesting();
        Singleton<CatalogoServicio>.resetForTesting();

        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Mecanica");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Jardineria");
        Singleton<CatalogoCategoria>.Instance.AgregarCategoria("Fletes");

        var Juan = Singleton<GestionUsuario>.Instance.CrearEmpleador("JuanCho", 12345678, "Juan", "Perez", "Masculino", 096123456, "a@gmail.com", "Parque Batlle", 1);
        var Juana = Singleton<GestionUsuario>.Instance.CrearTrabajador("Juana", 12345678, "Juana", "Perez", "Femenino", 096123456, "b@gmail.com", 2);
        var Pedro = Singleton<GestionUsuario>.Instance.CrearTrabajador("Pedro", 12345678, "Pedro", "Ramirez", "Masculino", 096123456, "c@gmail.com", 3);
        var Nacho = Singleton<GestionUsuario>.Instance.CrearTrabajador("Nacho", 12345678, "Nacho", "Gonzales", "Masculino", 096123456, "d@gmail.com", 4);
        var Kevin = Singleton<GestionUsuario>.Instance.CrearTrabajador("Kevin", 12345678, "Kevin", "Gonzales", "Masculino", 096123456, "e@gmail.com", 5);
        var Raul = Singleton<GestionUsuario>.Instance.CrearTrabajador("Raul", 12345678, "Raul", "Rodriguez", "Masculino", 096123456, "f@gmail.com", 6);

        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 4000, Juana, "8 de octubre");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Mecanica", "Arreglar autos", "Ejemplo", 3000, Pedro, "18 de julio");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Corta pasto", "Ejemplo", 1000, Juana, "Luis Alberto de Herrera");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Jardineria", "Floreria", "Ejemplo", 1000, Nacho, "Plaza independencia");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes largos", "Ejemplo", 1500, Kevin, "Sayago");
        Singleton<CatalogoServicio>.Instance.OfrecerServicio("Fletes", "Viajes pesados", "Ejemplo", 1200, Raul, "Maroñas"); 

        Calificacion c1 = new Calificacion(3, "Gracias");
        var s1 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 1);
        s1.Calificaciones.Add(c1);

        Calificacion c2 = new Calificacion(4, "Gracias");
        var s2 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 2);
        s2.Calificaciones.Add(c2);

        Calificacion c3 = new Calificacion(3, "Gracias");
        var s3 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 3);
        s3.Calificaciones.Add(c3);

        Calificacion c4 = new Calificacion(2, "Gracias");
        var s4 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 4);
        s4.Calificaciones.Add(c4);

        Calificacion c5 = new Calificacion(4, "Gracias");
        var s5 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 5);
        s5.Calificaciones.Add(c5);

        Calificacion c6 = new Calificacion(2, "Gracias");
        var s6 = Singleton<CatalogoServicio>.Instance.ListaServicio.Find(s => s.ServicioID == 6);
        s6.Calificaciones.Add(c6);

        List<Servicio> listaComparar = new List<Servicio>();
        listaComparar.Add(s1);
        listaComparar.Add(s2);
        listaComparar.Add(s3);
        listaComparar.Add(s4);
        listaComparar.Add(s5);
        listaComparar.Add(s6);

        Assert.AreEqual(Singleton<CatalogoServicio>.Instance.BuscarServicioSinFiltro(), listaComparar);
    }
}