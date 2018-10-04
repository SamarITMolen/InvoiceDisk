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
    public class ContactsController : ApiController
    {
        private ContactsEntities db = new ContactsEntities();

        // GET: api/Contacts
        public IQueryable<ContactsTable> GetContactsTables()
        {
            return db.ContactsTables;
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(ContactsTable))]
        public IHttpActionResult GetContactsTable(int id)
        {
            ContactsTable contactsTable = db.ContactsTables.Find(id);
            if (contactsTable == null)
            {
                return NotFound();
            }

            return Ok(contactsTable);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactsTable(int id, ContactsTable contactsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactsTable.ContactsId)
            {
                return BadRequest();
            }

            db.Entry(contactsTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsTableExists(id))
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

        // POST: api/Contacts
        [ResponseType(typeof(ContactsTable))]
        public IHttpActionResult PostContactsTable(ContactsTable contactsTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactsTables.Add(contactsTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactsTable.ContactsId }, contactsTable);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(ContactsTable))]
        public IHttpActionResult DeleteContactsTable(int id)
        {
            ContactsTable contactsTable = db.ContactsTables.Find(id);
            if (contactsTable == null)
            {
                return NotFound();
            }

            db.ContactsTables.Remove(contactsTable);
            db.SaveChanges();

            return Ok(contactsTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactsTableExists(int id)
        {
            return db.ContactsTables.Count(e => e.ContactsId == id) > 0;
        }
    }
}