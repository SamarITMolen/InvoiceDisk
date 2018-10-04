﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCQutationViewModel
    {

        public int? QutationID { get; set; }

        [Required]
        public string Qutation_ID { get; set; }
        public string RefNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> QutationDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<double> SubTotal { get; set; }
        public Nullable<double> DiscountAmount { get; set; }
        public Nullable<double> Vat { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public string CustomerNote { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> CompanyId { get; set; }


        public int? QutationDetailId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> Total { get; set; }
        public List<QutationDetailsTable> QutationDetailslist { get; set; }
        public List<MVCVatDetailsModel> mvcVatDetailsList { get; set; }

    }
}