﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceDisk.Models
{
    public class EmailModel
    {
        public string From { get; set;}
        public string ToEmail { get; set; }
        public string EmailBody { get; set; }

        public string Subject { get; set; }

        public string Attachment { get; set;}



    }
}