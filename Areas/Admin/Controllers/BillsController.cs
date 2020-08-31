using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Year_2.Areas.Admin.Infrastructure;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class BillsController : BaseController
    {
        private readonly QuanLyNhaHangDBContext db = new QuanLyNhaHangDBContext();
        // GET: Admin/Bills
        // GET: Bills
        public ActionResult Index()
        {
            var dao = new BillDao();
            var model = dao.ListAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Bill_Infor bill)
        {

            if (ModelState.IsValid)
            {
                var dao = new BillDao();
                if (dao.CheckName(bill.BillName))
                {
                    ModelState.AddModelError("", "Tên Hóa đơn đã tồn tại");
                }
                else
                {
                    long id = dao.Insert(bill);
                    if (id > 0)
                    {
                        SetAlert("Đăng kí Hóa đơn thành công", "success");
                        return RedirectToAction("Index", "Bills");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm Hóa đơn không thành công");
                    }
                }

            }
            return View(bill);
        }
        public ActionResult Edit(int ID)
        {
            var bill = new BillDao().ViewDetail(ID);
            return View(bill);
        }
        [HttpPost]
        public ActionResult Edit(Bill_Infor bill)
        {
            if (ModelState.IsValid)
            {
                var dao = new BillDao();
                int ID_Table_Get = int.Parse(TempData["ID_Table"].ToString());
                var result = dao.Update(bill, ID_Table_Get);
                if (result)
                {
                    SetAlert("Cập nhập hóa đơn thành công", "success");
                    return RedirectToAction("Index", "Bills");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập hóa đơn không thành công");
                }
            }
            return View("Index");
        }
        [CustomAuthorize("Admin","Manager")]
        public ActionResult Delete(int ID)
        {
            Bill_Infor bill = new BillDao().ViewDetail(ID);
            return View(bill);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            new MenuDao().Delete(ID);
            return RedirectToAction("Index");
        }
        public ActionResult Show(int ID)
        {
            var bills = new BillDao().Detail(ID);
            return View(bills);
        }

        public ActionResult CreateInfo()
        {
            ViewBag.ID_Bill = new SelectList(db.Bill_Infor, "ID_Bill", "BillName");
            ViewBag.Bill = db.Menus;
            return View();
        }
        [HttpPost]
        public ActionResult CreateInfo(int ID_Bill, int[] ID_Menus)
        {
            foreach (int itemID in ID_Menus)
            {

                Bill bill = new Bill();
                var dao = new BillDao();
                if (!dao.CheckID_Bill(ID_Bill) || !dao.CheckID_Menu(itemID))
                {
                    bill.ID_Bill = ID_Bill;
                    bill.ID_Menu = itemID;
                    
                    db.Bills.Add(bill);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Display()
        {
            ViewBag.ID_Bill = new SelectList(db.Bill_Infor, "ID_Bill", "BillName");
            ViewBag.Bill = db.Menus;
            return View();
        }
    }
}