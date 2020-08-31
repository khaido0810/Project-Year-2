namespace Project_Year_2.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Menu")]
    public partial class Menu
    {
        [Key]
        public long ID_Menu { get; set; }

        [StringLength(100)]

        [Display(Name = "Tên món ăn")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Giá tiền")]
        public double? Price { get; set; }
        
        [Display(Name = "Ảnh minh họa")]
        public string ImagePath { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(100)]

        [Display(Name = "Loại")]
        public string Type { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
