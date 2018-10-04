using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class ClassImage
    {
        public int? CompanyId { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public string ComapanyName { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string CompanyAddress { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string CompanyPhone { get; set; }
        [Required(ErrorMessage = "This filed is required")]
        public string CompanyCell { get; set; }
        public string CompanyEmail { get; set; }
        [DisplayName("Upload Image")]
        public string CompanyLogo { get; set; }
        public string CompanyTRN { get; set; }
        public string ComapnyFax { get; set; }
        public string CompanySubTitile { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}