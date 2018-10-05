using InvoiceDisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace InvoiceDisk.Controllers
{
    public class MVCSalesController : Controller
    {
        // GET: MVCSales
        public ActionResult Index()
        {
            try
            {
                IEnumerable<MVCSalesModel> ContactsList;
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Sales").Result;
                ContactsList = response.Content.ReadAsAsync<IEnumerable<MVCSalesModel>>().Result;
                return View(ContactsList);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View();
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                int contectId = 1;
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
                MVCContactsModel contectmodel = response.Content.ReadAsAsync<MVCContactsModel>().Result;

                int companyId = 2;

                HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
                MVCCompanyModel companyModel = responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;


                ViewBag.Contentdata = contectmodel;
                ViewBag.Companydata = companyModel;

                return View(new MVCSalesViewModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Sales/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCSalesViewModel>().Result);
            }
        }
    }
}