using Project_Year_2.Areas.Admin.Infrastructure;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("Admin","Manager")]
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var dao = new MenuDao();
            var model = dao.ListAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Menu food)
        {
            
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                if (dao.CheckName(food.Name))
                {
                    ModelState.AddModelError("", "Tên món ăn đã tồn tại");
                }
                else
                {
                    if (food.ImageFile != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(food.ImageFile.FileName);
                        string extension = Path.GetExtension(food.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        food.ImagePath = "~/Assets/Client/images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Assets/Client/images/"), fileName);
                        food.ImageFile.SaveAs(fileName);
                    }

                    long id = dao.Insert(food);
                    if (id > 0)
                    {
                        SetAlert("Đăng kí món ăn thành công", "success");
                        return RedirectToAction("Index", "Menu");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm món ăn không thành công");
                    }
                }
                
            }
            return View(food);
        }
        public ActionResult Edit(int ID)
        {
            var menu = new MenuDao().ViewDetail(ID);
            TempData["ID_Table"] = ID;
            return View(menu);
        }
        [HttpPost]
        public ActionResult Edit(Menu food)
        {
            if (ModelState.IsValid)
            {
                if (food.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(food.ImageFile.FileName);
                    string extension = Path.GetExtension(food.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    food.ImagePath = "~/Assets/Client/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/Client/images/"), fileName);
                    food.ImageFile.SaveAs(fileName);
                }
                var dao = new MenuDao();
                int ID_Table_Get = int.Parse(TempData["ID_Table"].ToString());
                var result = dao.Update(food, ID_Table_Get);
                if (result)
                {
                    SetAlert("Cập nhập thực đơn thành công", "success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập thực đơn không thành công");
                }
            }
            return View("Index");
        }
        public ActionResult Delete(int ID)
        {
            Menu food = new MenuDao().ViewDetail(ID);
            return View(food);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            new MenuDao().Delete(ID);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}