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
    public class QutationDetailController : ApiController
    {
        private QutationDetailsEntities db = new QutationDetailsEntities();

        // GET: api/QutationDetail
        public IQueryable<QutationDetailsTable> GetQutationDetailsTables()
        {
            return db.QutationDetailsTables;
        }

        // GET: api/QutationDetail/5
        [ResponseType(typeof(QutationDetailsTable))]
        public IHttpActionResult GetQutationDetailsTable(int id)
        {
            QutationDetailsTable qutationDetailsTable = db.QutationDetailsTables.Find(id);
            if (qutationDetailsTable == null)
            {
                return NotFound();
            }

            return Ok(qutationDetailsTable);
        }

        // PUT: api/QutationDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQutationDetailsTable(int id, QutationDetailsTable qutationDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qutationDetailsTable.QutationDetailId)
            {
                return BadRequest();
            }

            db.Entry(qutationDetailsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QutationDetailsTableExists(id))
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

        // POST: api/QutationDetail
        [ResponseType(typeof(QutationDetailsTable))]
        public IHttpActionResult PostQutationDetailsTable(QutationDetailsTable qutationDetailsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QutationDetailsTables.Add(qutationDetailsTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = qutationDetailsTable.QutationDetailId }, qutationDetailsTable);
        }

        // DELETE: api/QutationDetail/5
        [ResponseType(typeof(QutationDetailsTable))]
        public IHttpActionResult DeleteQutationDetailsTable(int id)
        {
            QutationDetailsTable qutationDetailsTable = db.QutationDetailsTables.Find(id);
            if (qutationDetailsTable == null)
            {
                return NotFound();
            }

            db.QutationDetailsTables.Remove(qutationDetailsTable);
            db.SaveChanges();

            return Ok(qutationDetailsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QutationDetailsTableExists(int id)
        {
            return db.QutationDetailsTables.Count(e => e.QutationDetailId == id) > 0;
        }
    }
}