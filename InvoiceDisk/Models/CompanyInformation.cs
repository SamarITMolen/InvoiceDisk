//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceDisk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyInformation
    {
        public int CompanyId { get; set; }
        public string ComapanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyCell { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyTRN { get; set; }
        public string ComapnyFax { get; set; }
        public string CompanySubTitile { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
    
        public virtual UserTable UserTable { get; set; }
    }
}
