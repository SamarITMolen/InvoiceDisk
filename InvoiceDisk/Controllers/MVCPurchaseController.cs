using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using InvoiceDisk.Models;
using System.Text;

namespace InvoiceDisk.Controllers
{
    public class MVCPurchaseController : Controller
    {
        // GET: MVCPurchase
        public ActionResult Index()
        {
            return View();
        }


        // GET: Company ReturnJson
        [HttpGet]
        public ActionResult Index1()
        {
            try
            {
                IEnumerable<MVCPurchaseOrderModel> PurchaseList;
                HttpResponseMessage respose = GlobalVeriables.WebApiClient.GetAsync("Purchase").Result;
                PurchaseList = respose.Content.ReadAsAsync<IEnumerable<MVCPurchaseOrderModel>>().Result;

                return Json(PurchaseList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            return View();

        }



        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                int contectId = 3;
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
                MVCContactsModel contectmodel = response.Content.ReadAsAsync<MVCContactsModel>().Result;

                int companyId = 2;

                HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
                MVCCompanyModel companyModel = responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;


                //var Logo = companyModel.CompanyLogo;
                //var Logos = new StringBuilder(Logo);

                //Logos.Remove(0, 2);
                //Logos.Insert(0,'~');
               
                //companyModel.CompanyLogo = Logos.ToString();               

                ViewBag.Contentdata = contectmodel;
                ViewBag.Companydata = companyModel;

                return View(new MVCPurchaseViewModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Purchase/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCPurchaseViewModel>().Result);
            }
        }

    }
}