using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceDisk.Models;
using System.Net.Http;
using System.IO;
using System.Drawing;

namespace InvoiceDisk.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
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
                IEnumerable<MVCCompanyModel> CompanyList;
                HttpResponseMessage respose = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations").Result;
                CompanyList = respose.Content.ReadAsAsync<IEnumerable<MVCCompanyModel>>().Result;

                return Json(CompanyList, JsonRequestBehavior.AllowGet);
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
               
                return View(new MVCCompanyModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCCompanyModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(MVCCompanyModel CompnayModel, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(CompnayModel);
            }
            else
            {
                if (file != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(file.FileName);
                    string Extention = Path.GetExtension(file.FileName);

                    filename = filename + DateTime.Now.ToString("yymmssfff") + Extention;

                    CompnayModel.CompanyLogo = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    file.SaveAs(filename);
                }
                
                if (CompnayModel.CompanyId == null)
                {

                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PostAsJsonAsync("CompanyInformations", CompnayModel).Result;
                    TempData["SuccessMessage"] = "Saved Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PutAsJsonAsync("CompanyInformations/" + CompnayModel.CompanyId, CompnayModel).Result;
                    TempData["SuccessMessage"] = "Updated Successfully";
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVeriables.WebApiClient.DeleteAsync("CompanyInformations/" + id.ToString()).Result;
                TempData["SuccessMessage"] = "Delete Successfully";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return View();
        }


        public ActionResult Details(int id)
        {
            try
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCCompanyModel>().Result);
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return View();
        }
    }
}
