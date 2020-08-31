using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Project_Year_2.Models.Dao
{
    public class MenuDao
    {
        QuanLyNhaHangDBContext context = null;
        public MenuDao()
        {
            context = new QuanLyNhaHangDBContext();
        }
        public long Insert(Menu food)
        {
            context.Menus.Add(food);
            context.SaveChanges();
            return food.ID_Menu;
        }
        public bool Update(Menu entity, int ID)
        {
            try
            {
                var food = context.Menus.Find(ID);
                food.Name = entity.Name;
                food.Price = entity.Price;
                food.Type = entity.Type;
                if (entity.ImagePath != null)
                {
                    food.ImagePath = entity.ImagePath;
                }
                food.Description = entity.Description;
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
            var food = context.Menus.Find(ID);
            context.Menus.Remove(food);
            context.SaveChanges();
            return true;
        }
        public Menu ViewDetail(int ID)
        {
            return context.Menus.Find(ID);
        }
        public List<Menu> ListAll()
        {
            return context.Menus.ToList();
        }
        public bool ChangeStatus(long id)
        {
            var food = context.Menus.Find(id);
            food.Status = !food.Status;
            context.SaveChanges();
            return food.Status;
        }
        public bool CheckName(string name)
        {
            return context.Menus.Count(x => x.Name == name) > 0;
        }
    }
    

}