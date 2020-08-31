namespace Project_Year_2.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill_Infor
    {
        [Key]
        public long ID_Bill { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên hóa đơn")]
        public string BillName { get; set; }

        [Display(Name = "Tổng số tiền")]
        public double? Total { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]

        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
    }
}
