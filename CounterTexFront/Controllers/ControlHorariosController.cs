using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ControlHorariosController : Controller
    {
        // GET: ControlHorarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}