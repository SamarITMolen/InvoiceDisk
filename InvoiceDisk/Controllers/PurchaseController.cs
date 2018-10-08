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
    public class PurchaseController : ApiController
    {
        private PurchaseEntities db = new PurchaseEntities();

        // GET: api/Purchase
        public IQueryable<PurchaseOrderTable> GetPurchaseOrderTables()
        {
            return db.PurchaseOrderTables;
        }

        // GET: api/Purchase/5
        [ResponseType(typeof(PurchaseOrderTable))]
        public IHttpActionResult GetPurchaseOrderTable(int id)
        {
            PurchaseOrderTable purchaseOrderTable = db.PurchaseOrderTables.Find(id);
            if (purchaseOrderTable == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrderTable);
        }

        // PUT: api/Purchase/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrderTable(int id, PurchaseOrderTable purchaseOrderTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrderTable.PurchaseOrderID)
            {
                return BadRequest();
            }

            db.Entry(purchaseOrderTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderTableExists(id))
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

        // POST: api/Purchase
        [ResponseType(typeof(PurchaseOrderTable))]
        public IHttpActionResult PostPurchaseOrderTable(PurchaseOrderTable purchaseOrderTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PurchaseOrderTables.Add(purchaseOrderTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrderTable.PurchaseOrderID }, purchaseOrderTable);
        }

        // DELETE: api/Purchase/5
        [ResponseType(typeof(PurchaseOrderTable))]
        public IHttpActionResult DeletePurchaseOrderTable(int id)
        {
            PurchaseOrderTable purchaseOrderTable = db.PurchaseOrderTables.Find(id);
            if (purchaseOrderTable == null)
            {
                return NotFound();
            }

            db.PurchaseOrderTables.Remove(purchaseOrderTable);
            db.SaveChanges();

            return Ok(purchaseOrderTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderTableExists(int id)
        {
            return db.PurchaseOrderTables.Count(e => e.PurchaseOrderID == id) > 0;
        }
    }
}