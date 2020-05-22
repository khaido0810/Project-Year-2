﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Home(int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging( page, pageSize);
            return View(model);
        }
        public ActionResult Index( int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                if (dao.CheckUserName(account.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else
                {
                    var encryptMd5Pass = Common.Encryptor.MD5Hash(account.Password);
                    account.Password = encryptMd5Pass;
                    long id = dao.Insert(account);
                    if (id > 0)
                    {
                        ViewBag.Success = "Đăng kí thành công";
                        account = new Account();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản không thành công");
                    }
                }                
            }
            return View(account);
        }
        public ActionResult Edit(int ID)
        {
            var account = new UserDao().ViewDetail(ID);
            return View(account);
        }
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                if (!string.IsNullOrEmpty(account.Password))
                {
                    var encryptMd5Pass = Common.Encryptor.MD5Hash(account.Password);
                    account.Password = encryptMd5Pass;
                }
                var result = dao.Update(account);
                if (result)
                {
                    SetAlert("Cập nhập tài khoản thành công", "success");
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập tài khoản không thành công");
                }
            }
            return View("Home");
        }
        [HttpDelete]
        public ActionResult Delete(int ID)
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
        
        [HttpGet]
        public new ActionResult Profile()
        {
            int id = int.Parse(Session["IDName"].ToString());
            var account = new UserDao().ViewDetail(id);
            return View(account);
        }

        public ActionResult Setting()
        {
            int id = int.Parse(Session["IDName"].ToString());
            var account = new UserDao().ViewDetail(id);
            return View(account);
        }
        [HttpPost]
        public ActionResult Setting(Account account)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                if (!string.IsNullOrEmpty(account.Password))
                {
                    var encryptMd5Pass = Common.Encryptor.MD5Hash(account.Password);
                    account.Password = encryptMd5Pass;
                }
                var result = dao.Update(account);
                if (result)
                {
                    SetAlert("Cập nhập tài khoản thành công", "success");
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập tài khoản không thành công");
                }
            }
            return View("Home");
        }
    }
}