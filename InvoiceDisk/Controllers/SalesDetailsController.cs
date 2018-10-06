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
    public class SalesDetailsController : ApiController
    {
        private SalesDetailsEntities db = new SalesDetailsEntities();

        // GET: api/SalesDetails
        public IQueryable<SalesDetailsTable> GetSalesDetailsTables()
        {
            return db.SalesDetailsTables;
        }

        // GET: api/SalesDetails/5
        [ResponseType(typeof(SalesDetailsTable))]
        public IHttpActionResult GetSalesDetailsTable(int id)
        {
            SalesDetailsTable salesDetailsTable = db.SalesDetailsTables.Find(id);
            if (salesDetailsTable == null)
            {
                return NotFound();
            }

            return Ok(salesDetailsTable);
        }

        // PUT: api/SalesDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalesDetailsTable(int id, SalesDetailsTable salesDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesDetailsTable.SalesDetailsId)
            {
                return BadRequest();
            }

            db.Entry(salesDetailsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesDetailsTableExists(id))
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

        // POST: api/SalesDetails
        [ResponseType(typeof(SalesDetailsTable))]
        public IHttpActionResult PostSalesDetailsTable(SalesDetailsTable salesDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesDetailsTables.Add(salesDetailsTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = salesDetailsTable.SalesDetailsId }, salesDetailsTable);
        }

        // DELETE: api/SalesDetails/5
        [ResponseType(typeof(SalesDetailsTable))]
        public IHttpActionResult DeleteSalesDetailsTable(int id)
        {
            SalesDetailsTable salesDetailsTable = db.SalesDetailsTables.Find(id);
            if (salesDetailsTable == null)
            {
                return NotFound();
            }

            db.SalesDetailsTables.Remove(salesDetailsTable);
            db.SaveChanges();

            return Ok(salesDetailsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesDetailsTableExists(int id)
        {
            return db.SalesDetailsTables.Count(e => e.SalesDetailsId == id) > 0;
        }
    }
}