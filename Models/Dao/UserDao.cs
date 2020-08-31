using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_Year_2.Models.Dao
{
    public class UserDao
    {
        readonly QuanLyNhaHangDBContext context = null;  
        public UserDao()
        {
            context = new QuanLyNhaHangDBContext();
        }
        public long Insert(Account account)
        {
            context.Accounts.Add(account);
            context.SaveChanges();
            return account.ID;
        }
        public bool Update(Account entity)
        {
            try
            {
                var user = context.Accounts.Find(entity.ID);
                user.ConfirmPassword = user.Password;
                user.User_Infor.Name = entity.User_Infor.Name;
                user.User_Infor.Avatar = entity.User_Infor.Avatar;
                user.User_Infor.PhoneNumber = entity.User_Infor.PhoneNumber;
                user.User_Infor.IdentityID = entity.User_Infor.IdentityID;
                user.User_Infor.Address = entity.User_Infor.Address;
                user.User_Infor.BirthDay = entity.User_Infor.BirthDay;
                user.User_Infor.Email = entity.User_Infor.Email;
                
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool ChangeRole(Account entity)
        {
            var user = context.Accounts.Find(entity.ID);
            user.ConfirmPassword = user.Password;
            try
            {
                user.Role = entity.Role;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int ID)
        {
            var account = context.Accounts.Find(ID);
            if (account.User_Infor != null)
            {
                context.User_Infors.Remove(account.User_Infor);

            }
            if (account != null)
            {
                context.Accounts.Remove(account);
            }
            context.SaveChanges();
            return true;
        }
        public List<Account> ListAll_Manager()
        {   
            return context.Accounts.Where(x => x.Role == "Manager").ToList();
        }
        public List<Account> ListAll_Staff()
        {
            return context.Accounts.Where(x => x.Role == "Staff").ToList();
        }
        public List<Account> ListAll()
        {
            return context.Accounts.ToList();
        }
        public Account GetByName(string UserName)
        {
            return context.Accounts.Where(x => x.UserName.Equals(UserName, StringComparison.CurrentCulture)).SingleOrDefault();
        }
        public Account ViewDetail(int ID)
        {
            return context.Accounts.Find(ID);
        }
        public int Login(string userName, string passWord)
        {
            var result = context.Accounts.Where(x => x.UserName.Equals(userName,StringComparison.CurrentCulture)).SingleOrDefault();
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public bool ChangeStatus(long id)
        {
            var user = context.Accounts.Find(id);
            user.Status = !user.Status;
            user.ConfirmPassword = user.Password;
            context.SaveChanges();
            return user.Status;
        }
        public bool CheckUserName(string username)
        {
            return context.Accounts.Count(x => x.UserName == username) > 0;
        }
        public bool UpdateAvatar(int id,Account account)
        {
            try
            {
                var user = context.Accounts.Find(id);
                user.ConfirmPassword = user.Password;
                user.User_Infor.Avatar = account.User_Infor.Avatar;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public long InsertInfor(User_Infor infor)
        {
            context.User_Infors.Add(infor);
            context.SaveChanges();
            return infor.ID;
        }
        public bool CheckEmail(string email)
        {
            return context.User_Infors.Count(x => x.Email == email) > 0;
        }
        public bool ResetPass(int ID)
        {
            var account = context.Accounts.Find(ID);
            var encryptMd5Pass = Common.Encryptor.MD5Hash(Convert.ToString(123));
            account.Password = encryptMd5Pass;
            account.ConfirmPassword = encryptMd5Pass;
            context.SaveChanges();
            return true;
        }
    }
}