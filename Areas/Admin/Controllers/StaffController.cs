using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Year_2.Areas.Admin.Infrastructure;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;

namespace Project_Year_2.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize("Admin")]
    public class StaffController : BaseController
    {
        // GET: Admin/Staff
        public ActionResult Home( )
        {
            var dao = new UserDao();
            var model = dao.ListAll_Staff();
            return View(model);
        }
        public ActionResult Index()
        {
            var dao = new UserDao();
            var model = dao.ListAll_Staff();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Account staff)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(staff.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                
                else
                {
                    try
                    {
                        var encryptMd5Pass = Common.Encryptor.MD5Hash(staff.Password);
                        var encryptMd5ConPass = Common.Encryptor.MD5Hash(staff.ConfirmPassword);
                        staff.Password = encryptMd5Pass;
                        staff.ConfirmPassword = encryptMd5ConPass;
                        long id = dao.Insert(staff);
                        if (id > 0)
                        {
                            ViewBag.Success = "Đăng kí thành công";
                            staff = new Account();
                        }
                        else
                        {
                            SetAlert("Thêm tài khoản không thành công", "error");
                        }
                    }
                    catch (Exception) { }
                    
                }
                
            }
            return View(staff);
        }
        public ActionResult Edit(int ID)
        {
            var staff = new UserDao().ViewDetail(ID);
            return View(staff);
        }
        [HttpPost]
        public ActionResult Edit(Account staff)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                if (!string.IsNullOrEmpty(staff.Password))
                {
                    var encryptMd5Pass = Common.Encryptor.MD5Hash(staff.Password);
                    staff.Password = encryptMd5Pass;
                }
                if (staff.User_Infor.AvatarFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(staff.User_Infor.AvatarFile.FileName);
                    string extension = Path.GetExtension(staff.User_Infor.AvatarFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    staff.User_Infor.Avatar = "~/Assets/Admin/img/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/Admin/img/"), fileName);
                    staff.User_Infor.AvatarFile.SaveAs(fileName);
                }
                else
                {
                    staff.User_Infor.Avatar = "~/Assets/Admin/img/Default_Avatar.png";

                }
                var result = dao.Update(staff);
                if (result)
                {
                    SetAlert("Cập nhập tài khoản thành công", "success");
                    return RedirectToAction("Home", "Staff");
                }
                else
                {
                    SetAlert("Cập nhập tài khoản không thành công", "error");
                }
            }
            return View("Home");
        }
        public ActionResult AddInfor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInfor(int ID, User_Infor entity)
        {
            var infor = new UserDao();
            entity.ID = ID;
            long user = 0;
            if (infor.CheckEmail(entity.Email))
            {
                ModelState.AddModelError("", "Email đã được sử dụng");
            }
            else
            {
                if (entity.AvatarFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(entity.AvatarFile.FileName);
                    string extension = Path.GetExtension(entity.AvatarFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    entity.Avatar = "~/Assets/Admin/img/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Assets/Admin/img/"), fileName);
                    entity.AvatarFile.SaveAs(fileName);
                }
                else
                {
                    entity.Avatar = "~/Assets/Admin/img/Default_Avatar.png";

                }
                user = infor.InsertInfor(entity);
            }
            
            if (user > 0)
            {
                SetAlert("Cập nhập tài khoản thành công", "success");
                return RedirectToAction("Home", "Staff");
            }
            else
            {
                SetAlert("Cập nhập tài khoản không thành công", "error");
            }
            return View();
        }
        public ActionResult Delete(int ID)
        {
            Account account = new UserDao().ViewDetail(ID);
            return View(account);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            new UserDao().Delete(ID);
            return RedirectToAction("Home");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public ActionResult ResetPass(int ID)
        {
            new UserDao().ResetPass(ID);
            return RedirectToAction("Home");
        }
    }
}