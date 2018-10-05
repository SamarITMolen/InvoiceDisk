using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCSalesDetailsModel
    {
        public int? SalesDetailsId { get; set; }
        public Nullable<int> SalesItemId { get; set; }
        public string SalesDescription { get; set; }
        public Nullable<int> SalesQunatity { get; set; }
        public Nullable<double> SalesItemRate { get; set; }
        public Nullable<double> SalesTotal { get; set; }
        public Nullable<double> SalesVat { get; set; }
        public Nullable<int> SalesInvoiceId { get; set; }
    }
}