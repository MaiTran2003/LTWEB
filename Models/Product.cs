using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoAn_LTWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageProducts { get; set; }
        [Required]
        public int Brand_Id { get; set; }
       // public Nullable<long> Quantity { get; set; }
        public virtual Brand Brand { get; set; }
    }
}