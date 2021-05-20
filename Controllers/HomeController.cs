using Aarco.Models;
using Aarco.Serrvice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Aarco.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Catalogo ObtenerCatalogo(string nombreCat, string filtro="1", int id=0)
        {
            Catalogo catalogo= null;
            RequestCatalogo request;
            try 
            {
                request = new RequestCatalogo {
                                                Filtro = filtro, 
                                                IdAplication = id,
                                                NombreCatalogo = nombreCat 
                                              };
                catalogo = new Services().GetCatalogo(request);
            }
            catch(Exception e)
            {

            }
            return catalogo;
        }

        public object Catalogo(string nombreCat, string filtro = "1", int id = 0)
        {
            Catalogo catalogo = ObtenerCatalogo(nombreCat, filtro, id);
            if (catalogo != null)
            {
                switch (nombreCat)
                {
                    case "Marca":
                        return !string.IsNullOrEmpty(catalogo.CatalogoJsonString)? JsonConvert.DeserializeObject<List<Marca>>(catalogo.CatalogoJsonString) : null;
                    case "Submarca":
                        return !string.IsNullOrEmpty(catalogo.CatalogoJsonString) ? JsonConvert.DeserializeObject<List<Submarca>>(catalogo.CatalogoJsonString) : null;
                    case "Modelo":
                        return !string.IsNullOrEmpty(catalogo.CatalogoJsonString) ? JsonConvert.DeserializeObject<List<Modelo>>(catalogo.CatalogoJsonString) : null;
                    case "DescripcionModelo":
                        return !string.IsNullOrEmpty(catalogo.CatalogoJsonString) ? JsonConvert.DeserializeObject<List<DescripcionModelo>>(catalogo.CatalogoJsonString) : null;
                    case "Sepomex":
                        return !string.IsNullOrEmpty(catalogo.CatalogoJsonString) ? JsonConvert.DeserializeObject<List<Sepomex>>(catalogo.CatalogoJsonString) : null;
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
            
        }
    }
}
