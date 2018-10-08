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
        public ActionResult Index1()
        {
            try
            {
                IEnumerable<MVCContactsModel> ContactsList;
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Contacts").Result;
                ContactsList = response.Content.ReadAsAsync<IEnumerable<MVCContactsModel>>().Result;
                return Json(ContactsList,JsonRequestBehavior.AllowGet);
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