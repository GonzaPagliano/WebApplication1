
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ArticulosController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ArticulosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var rubros = _context.Rubros.Where(p => p.Eliminado == false).ToList();
            rubros.Add(new Rubro { RubroID = 0, Descripcion = "[SELECCIONE UN RUBRO]" });
            ViewBag.RubroID = new SelectList(rubros.OrderBy(p => p.Descripcion), "RubroID", "Descripcion");

            List<SubRubro> subrubros = new List<SubRubro>();
            subrubros.Add(new SubRubro { SubRubroID = 0, Descripcion = "[SELECCIONE UN RUBRO]" });

            ViewBag.SubRubroID = new SelectList(subrubros.OrderBy(p => p.Descripcion), "SubRubroID", "Descripcion");


            return View();
        }

        //BUSCAR ARTICULOS

        public JsonResult BuscarArticulos()
        {
            var articulos = _context.Articulos.Include(r => r.SubRubro).Include(p => p.SubRubro.Rubro).ToList();

            // CREAMOS EL OBJETO DEVISTA EN FORMA DE LISTADO
            List<VistaArticulo> articulosMostrar = new List<VistaArticulo>();

            foreach (var articulo in articulos)
            {
                var vistaAticulo = new VistaArticulo
                {

                    ArticuloID = articulo.ArticuloID,
                    Descripcion = articulo.Descripcion,
                    RubroID = articulo.SubRubro.Rubro.RubroID,
                    SubrubroNombre = articulo.SubRubro.Descripcion,
                    UltAct = articulo.UltAct,
                    UltActString = articulo.UltAct.ToString("dd/MM/yyyy"),
                    PrecioCosto = articulo.PrecioCosto,
                    PorcentajeGanancia = articulo.PorcentajeGanancia,
                    Eliminado = articulo.Eliminado,
                    PrecioVenta = articulo.PrecioVenta,

                };
                articulosMostrar.Add(vistaAticulo);
            }
            return Json(articulosMostrar);
        }

        //GUARDAR ARTICULO

        public JsonResult GuardarArticulo(int ArticuloID, string Descripcion, decimal PrecioCosto, decimal PorcentajeGanancia, decimal PrecioVenta, int SubRubroID)
        {
            int resultado = 0;

            //SI ES 0 ES CORRECTO
            //SI ES 1 CAMPO DESCRIPCION ESTA VACIO
            //SI ES 2EL REGISTROYA EXISTECON LA MISMA DESCRIPCION


            if (!string.IsNullOrEmpty(Descripcion))
            {
                //TOUPPER HACER TODO MAYUSCULA
                Descripcion = Descripcion.ToUpper();

                if (ArticuloID == 0)
                {

                    //ANTESD DE CREAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMA DESCROPCION
                    if (_context.Articulos.Any(e => e.Descripcion == Descripcion & e.SubRubroID == SubRubroID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //ACA CREA EL ARTICULO
                        //PARA ESO CREAMOS UN OBJETO DE TIPO ARTICULONUEVO CON LOS DATOS NECESARIOS
                        var articuloNuevo = new Articulo
                        {
                            Descripcion = Descripcion,
                            UltAct = DateTime.Now,
                            PrecioCosto = PrecioCosto,
                            PorcentajeGanancia = PorcentajeGanancia,
                            PrecioVenta = PrecioVenta,
                            SubRubroID = SubRubroID,

                        };
                        _context.Add(articuloNuevo);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //ANTES DE EDITAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMADESCROPCION
                    if (_context.Articulos.Any(e => e.Descripcion == Descripcion && e.ArticuloID != ArticuloID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //EDITA EL REGISTRO
                        //BUSCAMOS EL REGISTRO EN LA BASE DE DATOS
                        var articulo = _context.Articulos.Single(m => m.ArticuloID == ArticuloID);

                        //CAMBIAMOS LA DESCRIPCIÓN POR LA QUE INGRESÓ EL USUARIO EN LA VISTA
                        articulo.Descripcion = Descripcion;
                        articulo.UltAct = DateTime.Now;
                        articulo.PrecioCosto = PrecioCosto;
                        articulo.PorcentajeGanancia = PorcentajeGanancia;
                        articulo.PrecioVenta = PrecioVenta;
                        articulo.SubRubroID = SubRubroID;
                        _context.SaveChanges();
                    }
                }
            }
            else
            {
                resultado = 1;
            }

            return Json(resultado);
        }

        //BUSCAR ARTICULOS

        public JsonResult BuscarArticulo(int ArticuloID)
        {
            var articulo = _context.Articulos.Include(s=> s.SubRubro).Include(r=> r.SubRubro.Rubro).FirstOrDefault(m => m.ArticuloID == ArticuloID);

            var vistaAticulo = new VistaArticulo
            {

                ArticuloID = articulo.ArticuloID,
                Descripcion = articulo.Descripcion,
                RubroID = articulo.SubRubro.Rubro.RubroID,
                SubrubroNombre = articulo.SubRubro.Descripcion,
                UltAct = articulo.UltAct,
                UltActString = articulo.UltAct.ToString("dd/MM/yyyy"),
                PrecioCosto = articulo.PrecioCosto,
                PorcentajeGanancia = articulo.PorcentajeGanancia,
                Eliminado = articulo.Eliminado,
                PrecioVenta = articulo.PrecioVenta,

            };

            return Json(vistaAticulo);
        }


        //ELIMINAR ARTICULOS

        public JsonResult EliminarArticulos(int ArticuloID, int Elimina)
        {
            bool resultado = true;

            var articulo = _context.Articulos.Find(ArticuloID);
            if (articulo != null)
            {
                if (Elimina == 0)
                {
                    articulo.Eliminado = false;
                }
                else
                {
                    articulo.Eliminado = true;
                }

                _context.SaveChanges();
            }

            return Json(resultado);
        }


    }        
}
