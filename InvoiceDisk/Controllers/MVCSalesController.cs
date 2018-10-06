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




        [HttpPost]
        public JsonResult Save(MVCSalesViewModel MVCSaleViewModel)
        {
            bool Status = false;
            #region            
            try
            {
                MVCSalesModel mvcSalesModel = new MVCSalesModel();

                if (ModelState.IsValid)
                {
                    mvcSalesModel.SalesInvoiceNumber = MVCSaleViewModel.SalesInvoiceNumber.ToString();
                    mvcSalesModel.CompanyId = 2;
                    mvcSalesModel.UserId = 1;
                    mvcSalesModel.SalesRefNumber = MVCSaleViewModel.SalesRefNumber.ToString();
                    mvcSalesModel.ISalesnvoiceDate = Convert.ToDateTime(MVCSaleViewModel.ISalesnvoiceDate);
                    mvcSalesModel.SalesDueDate = Convert.ToDateTime(MVCSaleViewModel.SalesDueDate);
                    mvcSalesModel.SalesSubtotal = Convert.ToDouble(MVCSaleViewModel.SalesSubtotal);
                    mvcSalesModel.SalesDiscountAmount = Convert.ToDouble(MVCSaleViewModel.SalesDiscountAmount);
                    mvcSalesModel.SalesTotalAmount = Convert.ToDouble(MVCSaleViewModel.SalesTotal);
                    mvcSalesModel.SalesCustomerNote = MVCSaleViewModel.SalesCustomerNote.ToString();
                    

                    var response = GlobalVeriables.WebApiClient.PostAsJsonAsync("SalesID", mvcSalesModel).Result;

                    IEnumerable<string> headerValues;
                    var userId = string.Empty;

                    var Salesid = "";
                    if (response.Headers.TryGetValues("Salesid", out headerValues))
                    {
                        Salesid = headerValues.FirstOrDefault();
                    }

                    List<SalesDetailsTable> SalesDetailsList = MVCSaleViewModel.SalesDetailslist;


                    foreach (SalesDetailsTable SDTList in SalesDetailsList)
                    {
                        SalesDetailsTable SaleDetailsTable = new SalesDetailsTable();
                        SaleDetailsTable.SalesItemId = Convert.ToInt32(SDTList.SalesItemId);
                        SaleDetailsTable.SalesInvoiceId = Convert.ToInt32(Salesid);
                        SaleDetailsTable.SalesDescription = SDTList.SalesDescription;
                        SaleDetailsTable.SalesQunatity = SDTList.SalesQunatity;
                        SaleDetailsTable.SalesItemRate = Convert.ToDouble(SDTList.SalesItemRate);
                        SaleDetailsTable.SalesTotal = Convert.ToDouble(SDTList.SalesTotal);
                        SaleDetailsTable.SalesVat = Convert.ToDouble(SDTList.SalesVat);
                        HttpResponseMessage responsses = GlobalVeriables.WebApiClient.PostAsJsonAsync("SalesDetails", SaleDetailsTable).Result;
                    }

                    List<MVCVatDetailsModel> mvcVatDetailsList = new List<MVCVatDetailsModel>();


                    Status = true;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            #endregion
            return new JsonResult { Data = new { Status = Status } };
        }

    }
}