namespace Project_Year_2.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Account")]
    public partial class Account
    {
        public long ID { get; set; }

        [StringLength(150)]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [StringLength(150)]
        [Display(Name = "Nhập lại Mật khẩu")]
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [StringLength(150)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [StringLength(30)]
        [Display(Name = "Vai trò")]
        public string Role { get; set; }

        public bool Status { get; set; }

        public virtual User_Infor User_Infor { get; set; }
    }
}
