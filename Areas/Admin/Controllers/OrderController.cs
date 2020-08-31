using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Year_2.Areas.Admin.Infrastructure;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index( )
        {
            var dao = new OrderDao();
            var model = dao.ListAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Order table)
        {
            if (ModelState.IsValid)
            {

                var dao = new OrderDao();
                long id = dao.Insert(table);
                if (id > 0)
                {
                    SetAlert("Thêm đơn đặt bàn thành công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn đặt bàn thành công");
                }
            }
            return View("Index");
        }
        public ActionResult Edit(int ID)
        {
            var table = new OrderDao().ViewDetail(ID);
            TempData["ID"] = ID;
            return View(table);
        }
        [HttpPost]
        public ActionResult Edit(Order table)
        {
            if (ModelState.IsValid)
            {

                var dao = new OrderDao();
                int IDGet = int.Parse(TempData["ID"].ToString());
                var result = dao.Update(table, IDGet);
                if (result)
                {
                    SetAlert("Cập nhập đơn đặt bàn thành công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập đặt bàn không thành công");
                }
            }
            return View("Index");
        }
        [CustomAuthorize("Admin","Manager")]
        public ActionResult Delete(int ID)
        {
            Order order = new OrderDao().ViewDetail(ID);
            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            new OrderDao().Delete(ID);
            return RedirectToAction("Index");
        }
    }
}