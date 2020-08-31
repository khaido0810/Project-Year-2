using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Year_2.Areas.Admin.Models
{
    public class SettingModel
    {
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Cần nhập mật khẩu cũ.")]
        public string OldPassword { get; set; }
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Cần nhập mật khẩu mới.")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu mới")]
        [Required(ErrorMessage = "Cần nhập lại mật khẩu mới.")]
        public string ConfirmPassword { get; set; }
    }
}