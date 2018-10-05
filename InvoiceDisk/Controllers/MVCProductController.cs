using InvoiceDisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace InvoiceDisk.Controllers
{
    public class MVCProductController : Controller
    {
        // GET: MVCProduct
        public ActionResult Index()
        {
            try
            {
                IEnumerable<MVCProductModel> ProductList;
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Product").Result;
                ProductList = response.Content.ReadAsAsync<IEnumerable<MVCProductModel>>().Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return View(ProductList);
                }
                else
                {
                    return View("~/Shared/StatusCode501.cshtml");
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return View();
        }


        [HttpGet]
        public ActionResult GetProduct()
        {

            HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Product").Result;
            var ProductList = response.Content.ReadAsAsync<IEnumerable<MVCProductModel>>().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return Json(ProductList, JsonRequestBehavior.AllowGet);
            }
            
            return View("");
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {

                return View(new MVCProductModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCProductModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MVCProductModel ProductModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ProductModel);
            }
            else
            {
                if (ProductModel.ProductId == null)
                {
                    ProductModel.Company_ID = 2;
                    ProductModel.AddedBy = 1;
                    ProductModel.AddedDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());

                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PostAsJsonAsync("Product", ProductModel).Result;
                    TempData["SuccessMessage"] = "Saved Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVeriables.WebApiClient.PutAsJsonAsync("Product/" + ProductModel.ProductId, ProductModel).Result;
                    TempData["SuccessMessage"] = "Updated Successfully";
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVeriables.WebApiClient.DeleteAsync("Product/" + id.ToString()).Result;
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
                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCProductModel>().Result);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View();
        }
    }
}