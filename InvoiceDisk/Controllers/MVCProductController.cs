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

            return View();
        }


        public ActionResult GetProductlist()
        {


            IEnumerable<MVCProductModel> ProductList;

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

                HttpResponseMessage response = GlobalVeriables.WebApiClient.GetAsync("Product").Result;
                ProductList = response.Content.ReadAsAsync<IEnumerable<MVCProductModel>>().Result;

                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search  on multiple field  
                    ProductList = ProductList.Where(p => p.ProductId.ToString().Contains(search) ||
                     p.ProductName.ToLower().Contains(search.ToLower()) ||
                     p.Description.ToString().ToLower().Contains(search.ToLower()) ||
                     p.SalePrice.ToString().Contains(search.ToLower()) ||
                     p.PurchasePrice.ToString().Contains(search) ||
                     p.Type.ToString().ToLower().Contains(search.ToLower()) ||
                     p.OpeningQuantity.ToString().Contains(search.ToLower())).ToList();
                }
                switch (sortColumn)
                {
                    case "ProductId":
                        ProductList = ProductList.OrderBy(c => c.ProductId);
                        break;
                    case "ProductName":
                        ProductList = ProductList.OrderBy(c => c.ProductName);
                        break;
                    case "Description":
                        ProductList = ProductList.OrderBy(c => c.Description);
                        break;

                    case "SalePrice":
                        ProductList = ProductList.OrderBy(c => c.SalePrice);
                        break;

                    case "PurchasePrice":
                        ProductList = ProductList.OrderBy(c => c.PurchasePrice);
                        break;

                    case "Type":

                        ProductList = ProductList.OrderBy(c => c.Type);
                        break;

                    case "OpeningQuantity":

                        ProductList = ProductList.OrderBy(c => c.OpeningQuantity);
                        break;


                    default:
                        ProductList = ProductList.OrderByDescending(c => c.ProductId);
                        break;
                }

                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                //{
                //    var v = CompanyList.OrderBy(c=>c.);

                //}
                int recordsTotal = recordsTotal = ProductList.Count();
                var data = ProductList.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Json(ProductList, JsonRequestBehavior.AllowGet);
            }

            return View();
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