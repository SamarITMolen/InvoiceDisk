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
    public class SalesController : ApiController
    {
        private SalesEntities2 db = new SalesEntities2();

        // GET: api/Sales
        public IQueryable<SalesTable> GetSalesTables()
        {
            return db.SalesTables;
        }

        // GET: api/Sales/5
        [ResponseType(typeof(SalesTable))]
        public IHttpActionResult GetSalesTable(int id)
        {
            SalesTable salesTable = db.SalesTables.Find(id);
            if (salesTable == null)
            {
                return NotFound();
            }

            return Ok(salesTable);
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalesTable(int id, SalesTable salesTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesTable.SalesInvoiceId)
            {
                return BadRequest();
            }

            db.Entry(salesTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesTableExists(id))
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

        // POST: api/Sales
        [ResponseType(typeof(SalesTable))]
        public HttpResponseMessage PostSalesTable([FromBody] SalesTable salesTable)
        {
            using (SalesEntities2 entities = new SalesEntities2())
            {
                entities.SalesTables.Add(salesTable);
                entities.SaveChanges();

                var massage = Request.CreateResponse(HttpStatusCode.Created, salesTable);
                massage.Headers.Location = new Uri(Request.RequestUri + salesTable.SalesInvoiceId.ToString());
                massage.Content.Headers.Add("SalesID", salesTable.SalesInvoiceId.ToString());
                massage.RequestMessage.Headers.Add("SalesID", salesTable.SalesInvoiceId.ToString());
                return massage;

            }
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(SalesTable))]
        public IHttpActionResult DeleteSalesTable(int id)
        {
            SalesTable salesTable = db.SalesTables.Find(id);
            if (salesTable == null)
            {
                return NotFound();
            }

            db.SalesTables.Remove(salesTable);
            db.SaveChanges();

            return Ok(salesTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesTableExists(int id)
        {
            return db.SalesTables.Count(e => e.SalesInvoiceId == id) > 0;
        }
    }
}