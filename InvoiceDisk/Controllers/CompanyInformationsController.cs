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
    public class CompanyInformationsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/CompanyInformations
        public IQueryable<CompanyInformation> GetCompanyInformations()
        {
            return db.CompanyInformations;
        }

        // GET: api/CompanyInformations/5
        [ResponseType(typeof(CompanyInformation))]
        public IHttpActionResult GetCompanyInformation(int id)
        {
            CompanyInformation companyInformation = db.CompanyInformations.Find(id);
            if (companyInformation == null)
            {
                return NotFound();
            }

            return Ok(companyInformation);
        }

        // PUT: api/CompanyInformations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyInformation(int id, CompanyInformation companyInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyInformation.CompanyId)
            {
                return BadRequest();
            }

            db.Entry(companyInformation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyInformationExists(id))
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

        // POST: api/CompanyInformations
        [ResponseType(typeof(CompanyInformation))]
        public IHttpActionResult PostCompanyInformation(CompanyInformation companyInformation)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    return BadRequest(ModelState);
                }
                catch(Exception ex)
                {
                    ex.ToString();
                }
            }

            db.CompanyInformations.Add(companyInformation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyInformation.CompanyId }, companyInformation);
        }

        // DELETE: api/CompanyInformations/5
        [ResponseType(typeof(CompanyInformation))]
        public IHttpActionResult DeleteCompanyInformation(int id)
        {
            CompanyInformation companyInformation = db.CompanyInformations.Find(id);
            if (companyInformation == null)
            {
                return NotFound();
            }

            db.CompanyInformations.Remove(companyInformation);
            db.SaveChanges();

            return Ok(companyInformation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyInformationExists(int id)
        {
            return db.CompanyInformations.Count(e => e.CompanyId == id) > 0;
        }
    }
}