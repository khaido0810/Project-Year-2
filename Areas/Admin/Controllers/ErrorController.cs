using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Admin/Error
        public ActionResult UnAuthorzied()
        {
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}