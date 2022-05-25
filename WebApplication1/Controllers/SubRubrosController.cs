
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
    public class SubRubrosController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SubRubrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var rubro = _context.Rubros.Where(p => p.Eliminado == false ).ToList();
            ViewBag.RubroID = new SelectList(rubro.OrderBy(p => p.Descripcion), "RubroID", "Descripcion");

            return View();
        }



        public JsonResult BuscarSubRubros()
        {
            var subRubros = _context.SubRubros.Include(r => r.Rubro).ToList();

            List<SubRubroMostrar> listadosubrubrosmostrar = new List<SubRubroMostrar>();

            foreach (var subrubro in subRubros)
            {
                var subRubroMostrar = new SubRubroMostrar
                {
                    SubRubroID = subrubro.SubRubroID,
                    Descripcion = subrubro.Descripcion,
                    RubroID = subrubro.RubroID,
                    RubroNombre = subrubro.Rubro.Descripcion,
                    Eliminado = subrubro.Eliminado,

                };
                listadosubrubrosmostrar.Add(subRubroMostrar);

            }

            return Json(listadosubrubrosmostrar);
        }



        //public JsonResult GuardarSubRubro(int SubRubroID, string Descripcion)
        //{

        //    bool resultado = true;

        //    if (SubRubroID == 0)
        //    {//ACA CREA SUBRUBRO
        //        var subrubroscrear = new SubRubro
        //        {

        //            Descripcion = Descripcion,

        //        };

        //        _context.Add(subrubroscrear);
        //        _context.SaveChanges();

        //    }

        //    return Json(resultado);

        //}

        public JsonResult GuardaSubRubro(int SubRubroID, string Descripcion, int RubroID)
        {
            int resultado = 0;

            //SI ES 0 ES CORRECTO
            //SI ES 1 CAMPO DESCRIPCION ESTA VACIO
            //SI ES 2EL REGISTROYA EXISTECON LA MISMA DESCRIPCION


            if (!string.IsNullOrEmpty(Descripcion))
            {
                //TOUPPER HACER TODO MAYUSCULA
                Descripcion = Descripcion.ToUpper();

                if (SubRubroID == 0)
                {

                    //ANTESD DE CREAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMA DESCROPCION
                    if (_context.SubRubros.Any(e => e.Descripcion == Descripcion & e.RubroID == RubroID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //CREA EL REGISTRO DE SUBRUBRO
                        //PARA ESO CREAMOS UN OBJETO DE TIPO RUBRO CON LOS DATOS NECESARIOS
                        var subrubro = new SubRubro
                        {
                            Descripcion = Descripcion,
                            RubroID = RubroID,
                        };
                        _context.Add(subrubro);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //ANTES DE EDITAR ELREGISTRO PREGUNTA SI EXISTE UNO CON LA MISMA DESCROPCION
                    if (_context.SubRubros.Any(e => e.Descripcion == Descripcion && e.SubRubroID != SubRubroID))
                    {
                        resultado = 2;
                    }
                    else
                    {
                        //EDITA EL REGISTRO
                        //BUSCAMOS EL REGISTRO EN LA BASE DE DATOS
                        var subrubro = _context.SubRubros.Single(m => m.SubRubroID == SubRubroID);

                        //CAMBIAMOS LA DESCRIPCIÓN POR LA QUE INGRESÓ EL USUARIO EN LA VISTA
                        subrubro.Descripcion = Descripcion;
                        subrubro.RubroID = RubroID;
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


        public JsonResult ComboSubRubro(int id)//RUBRO ID
        {
            //BUSCAR SUBRUBROS
            var subRubros = (from o in _context.SubRubros where o.RubroID == id && o.Eliminado == false select o).ToList();

            return Json(new SelectList(subRubros, "SubRubroID", "Descripcion"));
        }


        //BUSCAR RUBROS

        public JsonResult BuscarSubRubro(int SubRubroID)
        {
            var subrubro = _context.SubRubros.FirstOrDefault(m => m.SubRubroID == SubRubroID);

            return Json(subrubro);
        }



        //ELIMINAR RUBROS

        public JsonResult EliminarSubRubro(int SubRubroID, int Elimina)
        {
            bool resultado = true;

            var subrubro= _context.SubRubros.Find(SubRubroID);
            if (subrubro != null)
            {
                if (Elimina == 0)
                {
                    subrubro.Eliminado = false;
                }
                else
                {
                    subrubro.Eliminado = true;
                }

                _context.SaveChanges();
            }

            return Json(resultado);
        }

        private bool RubroExists(int id)
        {
            return _context.SubRubros.Any(e => e.SubRubroID == id);
        }
    }
}
