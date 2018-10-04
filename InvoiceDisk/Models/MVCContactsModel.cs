using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class MVCContactsModel
    {
        public int? ContactsId { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string ContactAddress { get; set; }       
        public Nullable<int> Company_Id { get; set; }
        public Nullable<int> UserId { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingPersonName { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingCompanyName { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingState { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingCountry { get; set; }
        public string BillingZibCode { get; set; }
        public string BillingEmail { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingVatTRN { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string BillingPhone { get; set; }
        public string BillingMobile { get; set; }
        public string BillingFax { get; set; }
        public string ShippingPersonName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCompanyName { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZIP { get; set; }
        public string ShippingEmail { get; set; }
        public string ShippingMobile { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingFax { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> Addeddate { get; set; }
        public string ShippingVatTRN { get; set; }
    }
}