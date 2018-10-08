using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InvoiceDisk.Models;

namespace InvoiceDisk.Controllers
{
    public class PurchaseOrderDetailsController : ApiController
    {
        private PurchaseOrderDetailsEntities db = new PurchaseOrderDetailsEntities();

        // GET: api/PurchaseOrderDetails
        public IQueryable<PurchaseOrderDetailsTable> GetPurchaseOrderDetailsTables()
        {
            return db.PurchaseOrderDetailsTables;
        }

        // GET: api/PurchaseOrderDetails/5
        [ResponseType(typeof(PurchaseOrderDetailsTable))]
        public IHttpActionResult GetPurchaseOrderDetailsTable(int id)
        {
            PurchaseOrderDetailsTable purchaseOrderDetailsTable = db.PurchaseOrderDetailsTables.Find(id);
            if (purchaseOrderDetailsTable == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrderDetailsTable);
        }

        // PUT: api/PurchaseOrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrderDetailsTable(int id, PurchaseOrderDetailsTable purchaseOrderDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrderDetailsTable.PurchaseOrderDetailsId)
            {
                return BadRequest();
            }

            db.Entry(purchaseOrderDetailsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderDetailsTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseOrderDetails
        [ResponseType(typeof(PurchaseOrderDetailsTable))]
        public IHttpActionResult PostPurchaseOrderDetailsTable(PurchaseOrderDetailsTable purchaseOrderDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrderDetailsTables.Add(purchaseOrderDetailsTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrderDetailsTable.PurchaseOrderDetailsId }, purchaseOrderDetailsTable);
        }

        // DELETE: api/PurchaseOrderDetails/5
        [ResponseType(typeof(PurchaseOrderDetailsTable))]
        public IHttpActionResult DeletePurchaseOrderDetailsTable(int id)
        {
            PurchaseOrderDetailsTable purchaseOrderDetailsTable = db.PurchaseOrderDetailsTables.Find(id);
            if (purchaseOrderDetailsTable == null)
            {
                return NotFound();
            }

            db.PurchaseOrderDetailsTables.Remove(purchaseOrderDetailsTable);
            db.SaveChanges();

            return Ok(purchaseOrderDetailsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderDetailsTableExists(int id)
        {
            return db.PurchaseOrderDetailsTables.Count(e => e.PurchaseOrderDetailsId == id) > 0;
        }
    }
}