using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceDisk.Models;

namespace InvoiceDisk.Controllers
{
    public class CheckController : ApiController
    {

        //api/Get/12
        public QutationTable Get(int id)
        {
            using (QutationEntities entities = new QutationEntities())
            {
                return entities.QutationTables.FirstOrDefault(e => e.QutationID == id);
            }
        } 


        public HttpResponseMessage post([FromBody] QutationTable qutationtable)
        {
            using (QutationEntities entities = new QutationEntities())
            {
                entities.QutationTables.Add(qutationtable);
                entities.SaveChanges();

                var massage =  Request.CreateResponse(HttpStatusCode.Created, qutationtable);
                massage.Headers.Location = new Uri(Request.RequestUri + qutationtable.QutationID.ToString());
                massage.Content.Headers.Add("idd",qutationtable.QutationID.ToString());
                massage.RequestMessage.Headers.Add("idd", qutationtable.QutationID.ToString());
                return massage;

            }
        }
    }
}
