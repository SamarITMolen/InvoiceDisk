using InvoiceDisk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceDisk.Controllers
{
    public class ExamplePDFController : Controller
    {
        // GET: ExamplePDF
        public ActionResult Index()
        {
            string fname = "PruebaPdf3" + Guid.NewGuid().ToString() + ".pdf";
            string filePath = Server.MapPath("~/PDF/"+ fname);
          
            Example E = new Example { Name = "Juan12", SurName = "Novales12" };
            var actionPDF = new Rotativa.ActionAsPdf("GeneraPDF", E)
            {
                FileName = filePath,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageMargins = { Left = 1, Right = 1 }
            };

            byte[] applicationPDFData = actionPDF.BuildPdf(ControllerContext);
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(applicationPDFData, 0, applicationPDFData.Length);
            return actionPDF;
        }


       


        public ActionResult GeneraPDF()
        {
            Example E = new Example { Name = "Juan2222", SurName = "Novales2222" };


            return View("Index", E);
        }

    }
}