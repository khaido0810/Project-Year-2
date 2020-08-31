using Project_Year_2.Areas.Admin.Infrastructure;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("Admin")]
    public class AuthorizedController : BaseController
    {
        // GET: Admin/Authorized
        public ActionResult Index()
        {
            var dao = new UserDao();
            var model = dao.ListAll();
            return View(model);
        }
        public ActionResult Change(int ID)
        {
            var dao = new UserDao();
            var model = dao.ViewDetail(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Change(Account account)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                var result = dao.ChangeRole(account);
                if (result)
                {
                    SetAlert("Phân quyền thành công", "success");
                    return RedirectToAction("Index", "Authorized");
                }
                else
                {
                    SetAlert( "Cập nhập tài khoản không thành công","error");
                }
            }
            return View("Index");
        }
    }
}