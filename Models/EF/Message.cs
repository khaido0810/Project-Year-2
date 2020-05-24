namespace Project_Year_2.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        [Key]
        public int ID_Message { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [Column("Message")]
        public string Message1 { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}