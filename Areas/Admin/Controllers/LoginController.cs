using Project_Year_2.Areas.Admin.Code;
using Project_Year_2.Areas.Admin.Models;
using Project_Year_2.Common;
using Project_Year_2.Models;
using Project_Year_2.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Project_Year_2.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));             
                if (result == 1)
                {
                    var user = dao.GetByName(model.UserName);
                    var userSession = new UserLogin
                    {
                        UserName = user.UserName,
                        UserID = user.ID
                    };
                    if (user.User_Infor != null)
                    {
                        Session["UserName"] = user.User_Infor.Name.ToString();
                    }
                    else
                    {
                        Session["UserName"] = "User";
                    }
                    Session["IDName"] = user.ID.ToString();
                    Session["User"] = user.UserName.ToString();
                    Session["Role"] = user.Role.ToString();
                    if (user.User_Infor != null)
                    {
                        Session["Avatar"] = user.User_Infor.Avatar.ToString();
                    }
                    else
                    {
                        Session["Avatar"] = "/Assets/Admin/img/Default_Avatar.png";
                    }
                    
                    Session.Add("USER_SESSION", userSession);
                    if (model.RememberMe)
                    {
                        // They do, so let's create an authentication cookie
                        var cookie = FormsAuthentication.GetAuthCookie(model.UserName, model.RememberMe);
                        // Since they want to be remembered, set the expiration for 30 days
                        cookie.Expires = DateTime.Now.AddDays(30);
                        // Store the cookie in the Response
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        // Otherwise set the cookie as normal
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tên đăng nhập không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không hợp lệ.");
                }

            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
        }
    }
}