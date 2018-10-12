using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceDisk.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Rotativa;
using System.IO;
using Rotativa.Options;

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
            List<MVCQutationModel> quationList = new List<MVCQutationModel>();
            try
            {
                #region
                int recordsTotal = 0;
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
                quationList = respose.Content.ReadAsAsync<List<MVCQutationModel>>().Result;
                List<MVCQutationModel> quationList1 = new List<MVCQutationModel>();
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search  on multiple field  
                    quationList = quationList.Where(p => p.QutationDate.ToString().Contains(search) ||
                    p.Qutation_ID.ToString().Contains(search) ||
                    p.RefNumber.ToString().Contains(search) ||
                    p.QutationDate.ToString().Contains(search) ||
                    p.QutationID.ToString().Contains(search)).ToList();

                }

                recordsTotal = recordsTotal = quationList.Count();
                var data = quationList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = 0 }, JsonRequestBehavior.AllowGet);

                //return  Json(ex.ToString(), JsonRequestBehavior.AllowGet);

            }

        }







        //MVCQutation/AddOrEdit

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            int contectId = 3;
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
            MVCContactsModel contectmodel = response.Content.ReadAsAsync<MVCContactsModel>().Result;

            int companyId = 2;

            HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
            MVCCompanyModel companyModel = responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;


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



        public ActionResult PrintView()
        {

            int contectId = 3;
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
            MVCContactsModel contectmodel = response.Content.ReadAsAsync<MVCContactsModel>().Result;

            int companyId = 2;

            HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
            MVCCompanyModel companyModel = responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;

            int Qutationid = 2;
            HttpResponseMessage responseQutation = GlobalVeriables.WebApiClient.GetAsync("Qutation/" + Qutationid.ToString()).Result;
            MVCQutationModel QutationModel = responseQutation.Content.ReadAsAsync<MVCQutationModel>().Result;

            HttpResponseMessage responseQutationDetailsList = GlobalVeriables.WebApiClient.GetAsync("QutationDetail/" + Qutationid.ToString()).Result;
            List<MVCQutationDetailsModel> QutationModelDetailsList = responseQutationDetailsList.Content.ReadAsAsync<List<MVCQutationDetailsModel>>().Result;



            ViewBag.Contentdata = contectmodel;
            ViewBag.Companydata = companyModel;
            ViewBag.QutationDat = QutationModel;
            ViewBag.QutationDatailsList = QutationModelDetailsList;


            //string fname = "PruebaPdf3" + Guid.NewGuid().ToString() + ".pdf";
            //string filePath = Server.MapPath("~/PDF/" + fname);


            //var actionPDF = new Rotativa.ActionAsPdf("GeneraPDF", QutationModel)
            //{
            //    FileName = filePath,
            //    PageSize = Size.A4,
            //    PageOrientation = Orientation.Landscape,
            //    PageMargins = { Left = 0, Right = 0 }


            //};

            //EmailModel m = new EmailModel();
           // bool sendemail = EmailController.email(p);

            return View();

        }





        [HttpPost]
        public ActionResult Save(MVCQutationViewModel mvcQutationViewModel)
        {
            bool Status = false;
            #region            
            try
            {
                MVCQutationModel mvcQutationModel = new MVCQutationModel();

                if (ModelState.IsValid)
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

                    var Qutationid = "";
                    if (response.Headers.TryGetValues("idd", out headerValues))
                    {
                        Qutationid = headerValues.FirstOrDefault();
                    }

                    List<QutationDetailsTable> QutationDetailsList = mvcQutationViewModel.QutationDetailslist;


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
            catch (Exception ex)
            {
                ex.ToString();
            }


            #endregion
            return new JsonResult { Data = new { Status = Status } };
        }

        public ActionResult GeneraPDF()
        {
            int contectId = 3;
            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + contectId.ToString()).Result;
            MVCContactsModel contectmodel = response.Content.ReadAsAsync<MVCContactsModel>().Result;

            int companyId = 2;

            HttpResponseMessage responseCompany = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + companyId.ToString()).Result;
            MVCCompanyModel companyModel = responseCompany.Content.ReadAsAsync<MVCCompanyModel>().Result;

            int Qutationid = 2;
            HttpResponseMessage responseQutation = GlobalVeriables.WebApiClient.GetAsync("Qutation/" + Qutationid.ToString()).Result;
            MVCQutationModel QutationModel = responseQutation.Content.ReadAsAsync<MVCQutationModel>().Result;

            HttpResponseMessage responseQutationDetailsList = GlobalVeriables.WebApiClient.GetAsync("QutationDetail/" + Qutationid.ToString()).Result;
            List<MVCQutationDetailsModel> QutationModelDetailsList = responseQutationDetailsList.Content.ReadAsAsync<List<MVCQutationDetailsModel>>().Result;
            ViewBag.Contentdata = contectmodel;
            ViewBag.Companydata = companyModel;
            ViewBag.QutationDat = QutationModel;
            ViewBag.QutationDatailsList = QutationModelDetailsList;

            var root = Server.MapPath("~/PDF/");
            var pdfname = String.Format("{0}.pdf", Guid.NewGuid().ToString());
            var path = Path.Combine(root, pdfname);
            string p = path.ToString();
            path = Path.GetFullPath(path);
            var something = new Rotativa.ViewAsPdf("PrintView", p) { FileName = "cv.pdf", SaveOnServerPath = path };

            //return new Rotativa.ActionAsPdf("PrintView", p) { FileName = "cv.pdf", SaveOnServerPath = path, };
            bool sendemail = EmailController.email(p);

            return something;
        }
    }
}
