using Project_Year_2.Areas.Admin.Models;
using Project_Year_2.Models.Dao;
using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Year_2.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Admin/Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Update()
        {
            QuanLyNhaHangDBContext context = new QuanLyNhaHangDBContext();
            
            var user = context.Accounts.Find(Convert.ToInt32(Session["IDName"].ToString()));
            
            return View(user);
        }
        [HttpPost]
        public ActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                Boolean result = false;
                try
                {
                    var dao = new UserDao();
                    QuanLyNhaHangDBContext context = new QuanLyNhaHangDBContext();
                    var user = context.Accounts.Find(Convert.ToInt32(Session["IDName"].ToString()));

                    if (!string.IsNullOrEmpty(account.Password))
                    {
                        var encryptMd5Pass = Common.Encryptor.MD5Hash(account.Password);
                        var encryptMd5ConPass = Common.Encryptor.MD5Hash(account.ConfirmPassword);
                        if(user.Password == encryptMd5Pass && user.Password == encryptMd5ConPass)
                        {
                            if (user.User_Infor != null)
                            {
                                user.User_Infor.Name = account.User_Infor.Name;
                                user.User_Infor.PhoneNumber = account.User_Infor.PhoneNumber;
                                user.User_Infor.IdentityID = account.User_Infor.IdentityID;
                                user.User_Infor.Address = account.User_Infor.Address;
                                user.User_Infor.BirthDay = account.User_Infor.BirthDay;
                                user.User_Infor.Email = account.User_Infor.Email;
                                context.SaveChanges();
                            }
                            else
                            {
                                account.User_Infor.ID = user.ID;
                                account.User_Infor.Avatar = "/Assets/Admin/img/Default_Avatar.png";
                                context.User_Infors.Add(account.User_Infor);
                                context.SaveChanges();
                            }
                            result = true;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Nhập mật khẩu sai");
                        }   
                    }
                }
                catch(Exception)
                {
                    result = false;
                }


                if (result)
                {
                    SetAlert("Cập nhập tài khoản cá nhân thành công", "success");
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Change(SettingModel account)
        {
            if (ModelState.IsValid)
            {
                Boolean result = false;
                try
                {
                    //var dao = new UserDao();
                    QuanLyNhaHangDBContext context = new QuanLyNhaHangDBContext();
                    var user = context.Accounts.Find(Convert.ToInt32(Session["IDName"].ToString()));

                    if (!string.IsNullOrEmpty(account.Password))
                    {
                        var encryptMd5Pass = Common.Encryptor.MD5Hash(account.OldPassword);
                        var encryptMd5NewPass = Common.Encryptor.MD5Hash(account.Password);
                        if (user.Password == encryptMd5Pass && account.Password == account.ConfirmPassword)
                        {
                            user.Password = encryptMd5NewPass;
                            user.ConfirmPassword = encryptMd5NewPass;
                            context.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Nhập mật khẩu sai");
                        }
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
                //catch (DbEntityValidationException ex)
                //{
                //    // Retrieve the error messages as a list of strings.
                //    var errorMessages = ex.EntityValidationErrors
                //            .SelectMany(x => x.ValidationErrors)
                //            .Select(x => x.ErrorMessage);

                //    // Join the list to a single string.
                //    var fullErrorMessage = string.Join("; ", errorMessages);

                //    // Combine the original exception message with the new one.
                //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //    // Throw a new DbEntityValidationException with the improved exception message.
                //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                //}

                if (result)
                {
                    SetAlert("Cập nhập tài khoản cá nhân thành công", "success");
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập không thành công");
                }
            }
            return RedirectToAction("Index");
        }
    }
}