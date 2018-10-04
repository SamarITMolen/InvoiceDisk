﻿using System;
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
    public class QutationController : ApiController
    {
        private QutationEntities db = new QutationEntities();

        // GET: api/Qutation
        public IQueryable<QutationTable> GetQutationTables()
        {
            return db.QutationTables;
        }
            
        

        // GET: api/Qutation/5
        [ResponseType(typeof(QutationTable))]
        public IHttpActionResult GetQutationTable(int id)
        {
            QutationTable qutationTable = db.QutationTables.Find(id);
            if (qutationTable == null)
            {
                return NotFound();
            }

            return Ok(qutationTable);
        }

        // PUT: api/Qutation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQutationTable(int id, QutationTable qutationTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qutationTable.QutationID)
            {
                return BadRequest();
            }

            db.Entry(qutationTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QutationTableExists(id))
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

        // POST: api/Qutation
        [ResponseType(typeof(QutationTable))]
        public HttpResponseMessage PostQutationTable([FromBody] QutationTable qutationtable)
        {
            using (QutationEntities entities = new QutationEntities())
            {
                entities.QutationTables.Add(qutationtable);
                entities.SaveChanges();

                var massage = Request.CreateResponse(HttpStatusCode.Created, qutationtable);
                massage.Headers.Location = new Uri(Request.RequestUri + qutationtable.QutationID.ToString());
                massage.Content.Headers.Add("idd", qutationtable.QutationID.ToString());
                massage.RequestMessage.Headers.Add("idd", qutationtable.QutationID.ToString());
                return massage;

            }
        }



        //public HttpResponseMessage Post([FromBody] QutationTable qutationTable)
        //{
        //    try
        //    {
        //        using (QutationEntities entities = new QutationEntities())
        //        {
        //            entities.QutationTables.Add(qutationTable);
        //            entities.SaveChanges();

        //            var message = Request.CreateResponse(HttpStatusCode.Created, qutationTable);
        //            message.Headers.Location = new Uri(Request.RequestUri +
        //                qutationTable.QutationID.ToString());

        //            return message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        // DELETE: api/Qutation/5
        [ResponseType(typeof(QutationTable))]
        public IHttpActionResult DeleteQutationTable(int id)
        {
            QutationTable qutationTable = db.QutationTables.Find(id);
            if (qutationTable == null)
            {
                return NotFound();
            }

            db.QutationTables.Remove(qutationTable);
            db.SaveChanges();

            return Ok(qutationTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QutationTableExists(int id)
        {
            return db.QutationTables.Count(e => e.QutationID == id) > 0;
        }
    }
}