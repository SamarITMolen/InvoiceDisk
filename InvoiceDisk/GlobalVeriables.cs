using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace InvoiceDisk
{
    public static class GlobalVeriables
    {
        public static HttpClient WebApiClient = new HttpClient();
        static GlobalVeriables()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:16732/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}