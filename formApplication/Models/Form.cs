namespace formApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Form
    {
        public int id { get; set; }

        public int users_id { get; set; }

        public DateTime? created_at { get; set; }

        [Required]
        public string context { get; set; }

        public virtual User User { get; set; }
    }
}
