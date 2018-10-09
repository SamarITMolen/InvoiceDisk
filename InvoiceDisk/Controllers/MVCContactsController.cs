using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceDisk.Models;
using System.Net.Http;

namespace InvoiceDisk.Controllers
{
    public class MVCContactsController : Controller
    {
        // GET: MVCContacts
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public JsonResult GetContactList()
        {
            IEnumerable<MVCContactsModel> ContactsList;
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" +
                Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                string search = Request.Form.GetValues("search[value]")[0];

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts").Result;
                ContactsList = response.Content.ReadAsAsync<IEnumerable<MVCContactsModel>>().Result;

                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search  on multiple field  
                    ContactsList = ContactsList.Where(p => p.ContactsId.ToString().Contains(search) ||
                    p.ContactName.ToLower().Contains(search.ToLower()) ||
                    p.BillingCountry.ToString().ToLower().Contains(search.ToLower()) ||
                    p.BillingCity.Contains(search.ToLower()) ||
                    p.Type.Contains(search.ToLower()) ||
                    p.BillingCompanyName.ToString().ToLower().Contains(search.ToLower()) ||
                    p.BillingVatTRN.Contains(search.ToLower())).ToList();
                }

                int recordsTotal = recordsTotal = ContactsList.Count();
                var data = ContactsList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {

                return View(new MVCContactsModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCContactsModel>().Result);
            }
        }


        [HttpPost]
        public ActionResult AddOrEdit(MVCContactsModel ContactModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ContactModel);
            }
            else
            {
                if (ContactModel.ContactsId == null)
                {
                    ContactModel.Company_Id = 2;
                    ContactModel.UserId = 1;
                    ContactModel.Addeddate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PostAsJsonAsync("Contacts", ContactModel).Result;
                    TempData["SuccessMessage"] = "Saved Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PutAsJsonAsync("Contacts/" + ContactModel.ContactsId, ContactModel).Result;
                    TempData["SuccessMessage"] = "Updated Successfully";
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVeriables.WebApiClient.DeleteAsync("Contacts/" + id.ToString()).Result;
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
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCContactsModel>().Result);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View();
        }

    }
}