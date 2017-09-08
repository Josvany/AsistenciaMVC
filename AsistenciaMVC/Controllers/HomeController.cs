using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.IO;


namespace AsistenciaMVC.Controllers
{

   [Authorize( Roles ="Administrador,OperadorA")]
    public class HomeController : Controller
    {
        //Instancias a clases de modelos
        private Alumno alumno = new Alumno();
        private AlumnoCurso alumno_curso = new AlumnoCurso();
        private Curso curso = new Curso();

        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Ver(int id)
        {           
            return View(alumno.Obtener(id));
        }

        public PartialViewResult Cursos(int Alumno_id)
        {
            //Listar los cursos de un alumno
            ViewBag.CursosElegidos = alumno_curso.Listar(Alumno_id);

            //Listar cursos disponibles del alumno
            ViewBag.cursos = curso.Todos(Alumno_id);

            //modelo

            alumno_curso.Alumno_id = Alumno_id;
            return PartialView(alumno_curso);
        }

        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.function = "CargarCursos()";   //esto invoca las funciones js que tengamos del lado de la vista
                }
            }

            return Json(rm);
        }

        public JsonResult Guardar(Alumno model)
        {
            var rm = model.Guardar();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }
            return Json(rm);
        }

        public ActionResult Crud(int id = 0)
        {
            return View(id == 0 ? new Alumno() : alumno.Obtener(id));
        }

        public ActionResult Eliminar(int id)
        {
            alumno.id = id;
            alumno.Eliminar();
            return Redirect("~/home");
        }
    }
}