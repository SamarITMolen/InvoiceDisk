using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceDisk.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace InvoiceDisk.Controllers
{
    public class MVCQutationController : Controller
    {
        // GET: MVCQutation
        public ActionResult Index()
        {
            return View();
        }

        //MVCQutation/Return Json

        [HttpPost]
        public JsonResult IndexQutation()
        {
            IEnumerable<MVCQutationModel> quationList;

            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();

                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" +
                Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                string search = Request.Form.GetValues("search[value]")[0];
                int skip = start != null ? Convert.ToInt32(start) : 0;

               
                HttpResponseMessage respose = GlobalVeriables.WebApiClient.GetAsync("Qutation").Result;
                quationList = respose.Content.ReadAsAsync<IEnumerable<MVCQutationModel>>().Result;

                int recordsTotal = recordsTotal = quationList.Count();
                var data = quationList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Json(null, JsonRequestBehavior.AllowGet);
     }
       
        
        //MVCQutation/AddOrEdit

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            int contectId = 3;
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
            MVCContactsModel contectmodel  = response.Content.ReadAsAsync<MVCContactsModel>().Result;

            int companyId = 2;

            HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
            MVCCompanyModel companyModel= responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;


            ViewBag.Contentdata = contectmodel;
            ViewBag.Companydata = companyModel;

            if (id == 0)
            {
                    

                return View(new MVCQutationViewModel());
            }
            else
            {
                HttpResponseMessage responses = GlobalVeriables.WebApiClient.GetAsync("Qutation/" + id.ToString()).Result;
                return View(responses.Content.ReadAsAsync<MVCQutationViewModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MVCQutationViewModel MVCQutationViewModel)
        {
            MVCQutationModel mvcQutationModel = new MVCQutationModel();

            if (!ModelState.IsValid)
            {
                return View(MVCQutationViewModel);
            }
            else
            {
                if (MVCQutationViewModel.QutationID == null)
                {

                    mvcQutationModel.Qutation_ID = MVCQutationViewModel.Qutation_ID;
                    mvcQutationModel.CompanyId = 2;
                    mvcQutationModel.UserId = 1;
                    mvcQutationModel.RefNumber = MVCQutationViewModel.RefNumber;
                    mvcQutationModel.QutationDate = MVCQutationViewModel.QutationDate;
                    mvcQutationModel.DueDate = MVCQutationViewModel.DueDate;
                    mvcQutationModel.SubTotal = MVCQutationViewModel.SubTotal;
                    mvcQutationModel.DiscountAmount = MVCQutationViewModel.DiscountAmount;                   
                    mvcQutationModel.TotalAmount = MVCQutationViewModel.TotalAmount;
                    mvcQutationModel.CustomerNote = MVCQutationViewModel.CustomerNote;
                    mvcQutationModel.Status = MVCQutationViewModel.Status;

                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PostAsJsonAsync("Qutation", mvcQutationModel).Result;
                    TempData["SuccessMessage"] = "Saved Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PutAsJsonAsync("Qutation/" + mvcQutationModel.QutationID, mvcQutationModel).Result;
                    TempData["SuccessMessage"] = "Updated Successfully";
                }
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public JsonResult Save(MVCQutationViewModel mvcQutationViewModel)
        {
            bool Status = false;
            #region            
            try
            {                
                MVCQutationModel mvcQutationModel = new MVCQutationModel();
               
                if(ModelState.IsValid)
                { 
                mvcQutationModel.Qutation_ID = mvcQutationViewModel.Qutation_ID.ToString();
                mvcQutationModel.CompanyId = 2;
                mvcQutationModel.UserId = 1;
                mvcQutationModel.RefNumber = mvcQutationViewModel.RefNumber.ToString();
                mvcQutationModel.QutationDate = Convert.ToDateTime(mvcQutationViewModel.QutationDate);
                mvcQutationModel.DueDate = Convert.ToDateTime(mvcQutationViewModel.DueDate);
                mvcQutationModel.SubTotal = Convert.ToDouble(mvcQutationViewModel.SubTotal);
                mvcQutationModel.DiscountAmount = Convert.ToDouble(mvcQutationViewModel.DiscountAmount);              
                mvcQutationModel.TotalAmount = Convert.ToDouble(mvcQutationViewModel.Total);
                mvcQutationModel.CustomerNote = mvcQutationViewModel.CustomerNote.ToString();
                mvcQutationModel.Status = "Open";

                var response = GlobalVeriables.WebApiClient.PostAsJsonAsync("Qutation", mvcQutationModel).Result;

                IEnumerable<string> headerValues;
                var userId = string.Empty;

                var Qutationid ="";
                if (response.Headers.TryGetValues("idd", out headerValues))
                {
                   Qutationid = headerValues.FirstOrDefault();
                }

                List <QutationDetailsTable> QutationDetailsList = mvcQutationViewModel.QutationDetailslist;


                foreach (QutationDetailsTable QDTList in QutationDetailsList)
                {
                    QutationDetailsTable QtDetails = new QutationDetailsTable();
                    QtDetails.ItemId = Convert.ToInt32(QDTList.ItemId);
                    QtDetails.QutationID = Convert.ToInt32(Qutationid); 
                    QtDetails.Description = QDTList.Description;
                    QtDetails.Quantity = QDTList.Quantity;
                    QtDetails.Rate = Convert.ToDouble(QDTList.Rate);
                    QtDetails.Total = Convert.ToDouble(QDTList.Total);
                    QtDetails.Vat = Convert.ToDouble(QDTList.Vat);
                    HttpResponseMessage responsses = GlobalVeriables.WebApiClient.PostAsJsonAsync("QutationDetail", QtDetails).Result;
                }

                    List<MVCVatDetailsModel> mvcVatDetailsList = new List<MVCVatDetailsModel>();


                    Status = true;
                    }

            }
            catch(Exception ex)
            {
                ex.ToString();
            }
           
            #endregion
            return new JsonResult { Data = new { Status = Status } };
        }

    }
}