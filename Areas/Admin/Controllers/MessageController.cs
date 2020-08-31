using Project_Year_2.Areas.Admin.Infrastructure;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class MessageController : BaseController
    {
        // GET: Admin/Message
        public ActionResult Index()
        {
            var dao = new MessageDao();
            var model = dao.ListAll();
            return View(model);
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MessageDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }

        [CustomAuthorize("Admin","Manager")]
        public ActionResult Delete(int ID)
        {
            Message message = new MessageDao().ViewDetail(ID);
            return View(message);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            new MessageDao().Delete(ID);
            return RedirectToAction("Index");
        }
    }

}