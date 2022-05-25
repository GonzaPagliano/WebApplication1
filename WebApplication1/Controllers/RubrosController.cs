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
    public class RubrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RubrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rubros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rubros.ToListAsync());
        }

        

        public JsonResult BuscarRubros()
        {
            var rubros = _context.Rubros.ToList();

            return Json(rubros);
        }

        //public JsonResult GuardarRubro(int RubroID, string Descripcion)
        //{
         
        //    bool resultado = true;
            
        //    if (RubroID == 0)
        //    {//ACA CREA RUBRO
        //        var rubroscrear = new Rubro
        //        {

        //            Descripcion = Descripcion,

        //        };

        //        _context.Add(rubroscrear);
        //        _context.SaveChanges();

        //    }

        //    return Json(resultado);

        //}        

        //GUARDAR RUBROS

        public JsonResult GuardaRubro(int RubroID, string Descripcion)
        {
            int resultado = 0;

            //SI ES 0 ES CORRECTO
            //SI ES 1 CAMPO DESCRIPCION ESTA VACIO
            //SI ES 2EL REGISTROYA EXISTECON LA MISMA DESCRIPCION


            if (!string.IsNullOrEmpty(Descripcion))
            {

                Descripcion = Descripcion.ToUpper();

                if (RubroID == 0)
                {

                    //ANTESD DE CREAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMADESCROPCION
                    if (_context.Rubros.Any(e => e.Descripcion == Descripcion))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //CREA EL REGISTRO DE RUBRO
                        //PARA ESO CREAMOS UN OBJETO DE TIPO RUBRO CON LOS DATOS NECESARIOS
                        var rubro = new Rubro
                        {
                            Descripcion = Descripcion
                        };
                        _context.Add(rubro);
                        _context.SaveChanges();
                    }                    
                }
                else
                {
                    //ANTESD DE EDITAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMADESCROPCION
                    if (_context.Rubros.Any(e => e.Descripcion == Descripcion))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //EDITA EL REGISTRO
                        //BUSCAMOS EL REGISTRO EN LA BASE DE DATOS
                        var rubro = _context.Rubros.Single(m => m.RubroID == RubroID);
                        //CAMBIAMOS LA DESCRIPCIÓN POR LA QUE INGRESÓ EL USUARIO EN LA VISTA
                        rubro.Descripcion = Descripcion;
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

        //BUSCAR RUBROS

        public JsonResult BuscarRubro(int RubroID)
        {
            var rubro = _context.Rubros.FirstOrDefault(m => m.RubroID == RubroID);

            return Json(rubro);
        }

        //ELIMINAR RUBROS

        public JsonResult EliminarRubro(int RubroID, int Elimina)
        {
            bool resultado = true;

            var rubro = _context.Rubros.Find(RubroID);
            if (rubro != null)
            {
                if (Elimina == 0)
                {
                    rubro.Eliminado = false;
                }
                else
                {
                    rubro.Eliminado = true;
                }
                
                _context.SaveChanges();
            }

            return Json(resultado);
        }

        private bool RubroExists(int id)
        {
            return _context.Rubros.Any(e => e.RubroID == id);
        }
    }
}
