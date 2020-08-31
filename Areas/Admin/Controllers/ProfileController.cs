using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Admin/Profile
        public ActionResult Index()
        {
            int id = int.Parse(Session["IDName"].ToString());
            var account = new UserDao().ViewDetail(id);
            return View(account);
        }
        [HttpPost]
        public ActionResult Index(Account account)
        {
            if (account.User_Infor.AvatarFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(account.User_Infor.AvatarFile.FileName);
                string extension = Path.GetExtension(account.User_Infor.AvatarFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                account.User_Infor.Avatar = "~/Assets/Admin/img/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Assets/Admin/img/"), fileName);
                account.User_Infor.AvatarFile.SaveAs(fileName);
            }
            else
            {
                account.User_Infor.Avatar = "/Assets/Admin/img/Default_Avatar.png";
            }
            int id = int.Parse(Session["IDName"].ToString());
            var user = new UserDao().UpdateAvatar(id, account);
            if (user)
            {
                SetAlert("Cập nhập ảnh đại diện thành công", "success");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhập tài khoản không thành công");
            }
            Session["Avatar"] = account.User_Infor.Avatar.ToString();
            return View(account);
        }
    }
}