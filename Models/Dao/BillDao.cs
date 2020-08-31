using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Project_Year_2.Models.Dao
{
    public class BillDao
    {
        private QuanLyNhaHangDBContext context = null;
        public BillDao()
        {
            context = new QuanLyNhaHangDBContext();
        }
        public long Insert(Bill_Infor bill)
        {
            context.Bill_Infor.Add(bill);
            context.SaveChanges();
            return bill.ID_Bill;
        }
        public bool Update(Bill_Infor entity, int ID)
        {
            try
            {
                var bill = context.Bill_Infor.Find(ID);
                bill.BillName = entity.BillName;
                bill.Total = entity.Total;
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
            var bill = context.Bill_Infor.Find(ID);
            context.Bill_Infor.Remove(bill);
            context.SaveChanges();
            return true;
        }
        public Bill_Infor ViewDetail(int ID)
        {
            return context.Bill_Infor.Find(ID);
        }
        public double? ViewTotal(int ID)
        {
            var bill = context.Menus.Find(ID);
            return bill.Price;
        }
        public IEnumerable<Bill> Detail(int ID)
        {
            return context.Bills.Include(b => b.Bill_Infor).Include(b => b.Menu).Where(x=>x.ID_Bill == ID);
        }
        public List<Bill_Infor> ListAll()
        {
            return context.Bill_Infor.ToList();
        }
        public bool CheckName(string name)
        {
            return context.Bill_Infor.Count(x => x.BillName == name) > 0;
        }
        public bool CheckID_Bill(int id)
        {
            return context.Bills.Count(x => x.ID_Bill == id) > 0;
        }
        public bool CheckID_Menu(int id)
        {
            return context.Bills.Count(x => x.ID_Menu == id) > 0;
        }
    }
    

}