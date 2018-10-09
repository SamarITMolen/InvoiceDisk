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







        [HttpPost]
        public ActionResult Index1()
        {
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

                IEnumerable<MVCCompanyModel> CompanyList;
                HttpResponseMessage respose = GlobalVeriables.WebApiClient.GetAsync("CompanyInformations").Result;
                CompanyList = respose.Content.ReadAsAsync<IEnumerable<MVCCompanyModel>>().Result;

                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search  on multiple field  
                    CompanyList = CompanyList.Where(p => p.CompanyId.ToString().Contains(search) ||
                    p.ComapanyName.ToLower().Contains(search.ToLower()) ||
                    p.CompanyAddress.ToString().ToLower().Contains(search.ToLower()) ||
                    p.CompanyPhone.Contains(search.ToLower()) ||
                    p.CompanyCell.Contains(search.ToLower()) ||
                    p.CompanyEmail.ToString().ToLower().Contains(search.ToLower()) ||
                    p.CompanyTRN.Contains(search.ToLower())).ToList();
                }
                switch (sortColumn)
                {
                    case "ComapanyName":
                        CompanyList = CompanyList.OrderBy(c => c.ComapanyName);
                        break;
                    case "CompanyAddress":
                        CompanyList = CompanyList.OrderBy(c => c.CompanyAddress);
                        break;
                    case "CompanyPhone":
                        CompanyList = CompanyList.OrderBy(c => c.CompanyPhone);
                        break;

                    case "CompanyCell":
                        CompanyList = CompanyList.OrderBy(c => c.CompanyCell);
                        break;

                    case "CompanyEmail":
                        CompanyList = CompanyList.OrderBy(c => c.CompanyEmail);
                        break;

                    case "CompanyTRN":

                        CompanyList = CompanyList.OrderBy(c => c.CompanyTRN);
                        break;

                    default:
                        CompanyList = CompanyList.OrderByDescending(c => c.CompanyId);
                        break;
                }

                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                //    var v = CompanyList.OrderBy(c=>c.);

                //}
                int recordsTotal = recordsTotal = CompanyList.Count();
                var data = CompanyList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View();
        }
    }
}
