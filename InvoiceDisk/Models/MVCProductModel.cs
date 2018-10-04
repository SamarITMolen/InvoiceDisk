using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCProductModel
    {
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public Nullable<double> SalePrice { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public Nullable<double> PurchasePrice { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public Nullable<int> OpeningQuantity { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
    }
}