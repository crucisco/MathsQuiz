using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrucisCo.MathsQuizWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Multiplication()
        {
            ViewBag.Message = "These are your multiplication quiz results.";
            return View();
        }

        public ActionResult Decimal()
        {
            ViewBag.Message = "These are your decimal quiz results.";
            return View();
        }

        public ActionResult UserStats()
        {
            ViewBag.Message = "User Statistics";

            return View();
        }
    }
}