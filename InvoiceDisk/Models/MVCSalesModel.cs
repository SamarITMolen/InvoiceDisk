using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCSalesModel
    {
        public int? SalesInvoiceId { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public string SalesRefNumber { get; set; }
        public Nullable<System.DateTime> ISalesnvoiceDate { get; set; }
        public string SalesDueDate { get; set; }
        public Nullable<int> SalesPayementTerm { get; set; }
        public Nullable<double> SalesSubtotal { get; set; }
        public Nullable<double> SalesDiscount { get; set; }
        public Nullable<double> SalesDiscountAmount { get; set; }
        public Nullable<double> SalesTotalAmount { get; set; }
        public string SalesCustomerNote { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
    }
}