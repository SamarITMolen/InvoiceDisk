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
        InvoiceDiskProductEntities dbp = new InvoiceDiskProductEntities();
        // GET: api/QutationDetail
        public IQueryable<QutationDetailsTable> GetQutationDetailsTables()
        {
            return db.QutationDetailsTables;
        }

        // GET: api/QutationDetail/5
        [ResponseType(typeof(List<MVCQutationViewModel>))]
        public IHttpActionResult GetQutationDetailsTable(int id)
        {
            List<MVCQutationViewModel> qutationDetailsTable = new List<MVCQutationViewModel>();
            var obj = db.QutationDetailsTables.ToList().Where(c => c.QutationID == id).ToList();

            var query = (from pd in db.QutationDetailsTables
                         join p in db.ProductTables on pd.ItemId equals p.ProductId
                         where pd.QutationID == id
                         select new MVCQutationViewModel { ItemId = pd.ItemId, QutationID = pd.QutationID, Rate = pd.Rate,
                             Quantity = pd.Quantity, Vat = pd.Vat, ItemName=p.ProductName,
                             Total=pd.Total, QutationDetailId= pd.QutationDetailId
                          
                        }).ToList();




            if (query == null)
            {
                return NotFound();
            }
            return Ok(query);
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
                return StatusCode(HttpStatusCode.OK);
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