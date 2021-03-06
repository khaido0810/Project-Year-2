﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;
using Project_Year_2.Models.EF;
using Project_Year_2.Models.Dao;
using Common;

namespace Project_Year_2.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View("Index", "Home");
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
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/newOrder.html"));
                    content = content.Replace("{{FirstName}}", table.FirstName);
                    content = content.Replace("{{LastName}}", table.LastName);
                    content = content.Replace("{{Phone}}", table.PhoneNumber);
                    content = content.Replace("{{Email}}", table.Email);
                    content = content.Replace("{{PeopleCount}}", table.PeopleCount.ToString());
                    content = content.Replace("{{Date}}", table.Date);
                    content = content.Replace("{{Time}}", table.Time);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    try
                    {

                        new MailHelper().SendMail(table.Email, "Đơn đặt bàn mới từ My Restaurant", content);
                        new MailHelper().SendMail(toEmail, "Đơn đặt bàn mới từ My Restaurant", content);
                    }
                    catch (Exception) { }
                    
                    ViewData["success"] = "Thêm Đơn đặt bàn thành công";
                    
                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn đặt bàn không thành công");
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}