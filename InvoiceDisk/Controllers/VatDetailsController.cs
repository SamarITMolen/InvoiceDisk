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
    public class VatDetailsController : ApiController
    {
        private InvoiceDiskEntities db = new InvoiceDiskEntities();

        // GET: api/VatDetails
        public IQueryable<VatDetailsTable> GetVatDetailsTables()
        {
            return db.VatDetailsTables;
        }

        // GET: api/VatDetails/5
        [ResponseType(typeof(VatDetailsTable))]
        public IHttpActionResult GetVatDetailsTable(int id)
        {
            VatDetailsTable vatDetailsTable = db.VatDetailsTables.Find(id);
            if (vatDetailsTable == null)
            {
                return NotFound();
            }

            return Ok(vatDetailsTable);
        }

        // PUT: api/VatDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVatDetailsTable(int id, VatDetailsTable vatDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vatDetailsTable.VatID)
            {
                return BadRequest();
            }

            db.Entry(vatDetailsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VatDetailsTableExists(id))
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

        // POST: api/VatDetails
        [ResponseType(typeof(VatDetailsTable))]
        public IHttpActionResult PostVatDetailsTable(VatDetailsTable vatDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VatDetailsTables.Add(vatDetailsTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vatDetailsTable.VatID }, vatDetailsTable);
        }

        // DELETE: api/VatDetails/5
        [ResponseType(typeof(VatDetailsTable))]
        public IHttpActionResult DeleteVatDetailsTable(int id)
        {
            VatDetailsTable vatDetailsTable = db.VatDetailsTables.Find(id);
            if (vatDetailsTable == null)
            {
                return NotFound();
            }

            db.VatDetailsTables.Remove(vatDetailsTable);
            db.SaveChanges();

            return Ok(vatDetailsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VatDetailsTableExists(int id)
        {
            return db.VatDetailsTables.Count(e => e.VatID == id) > 0;
        }
    }
}