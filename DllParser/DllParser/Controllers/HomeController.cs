using DllParser.Core;
using DllParser.Core.Models;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DllParser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SomeOtherPage()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            return View();
        }

        public JsonResult UploadFile(string id)
        {
            List<TypeModel> items = new List<TypeModel>();

            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var fileName = System.IO.Path.GetFileName(file);
                        var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        LibraryParser lp = new LibraryParser(@"C:\Users\Aliaksei_Kazlou\Documents\Visual Studio 2013\Projects\DllParser\DllParser\bin\DllParser.dll");
                        items = lp.Parse().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(items);
        }
    }
}
