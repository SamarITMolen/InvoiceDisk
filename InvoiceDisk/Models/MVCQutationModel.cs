﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCQutationModel
    {
        public int? QutationID { get; set; }
        public string Qutation_ID { get; set; }
        public string RefNumber { get; set; }
        public Nullable<DateTime> QutationDate { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public Nullable<double> SubTotal { get; set; }
        public Nullable<double> DiscountAmount { get; set; }       
        public Nullable<double> TotalAmount { get; set; }
        public string CustomerNote { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public Nullable<float> TotalVat6 { get; set; }
        public Nullable<float> TotalVat21 { get; set; }


    }
}