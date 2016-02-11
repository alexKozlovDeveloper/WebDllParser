using DllParser.Configuration;
using DllParser.Core;
using DllParser.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public JsonResult UploadFile()
        {
            List<TypeModel> items = new List<TypeModel>();

            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        Stream stream = fileContent.InputStream;
                        string fileName = Path.GetRandomFileName();
                        string path = Path.Combine(Server.MapPath(Keys.AppDataPath), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        LibraryParser lp = new LibraryParser(path);
                        items = lp.Parse().ToList();

                        Session[Keys.AssemblyItems] = lp.TypesAsDictionary;
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

        public JsonResult GetTypeInfo(string name)
        {
            Dictionary<string, TypeModel> items = Session[Keys.AssemblyItems] as Dictionary<string, TypeModel>;

            if (items == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Not found Types");
            }

            try
            {
                return Json(items[name]);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed: " + ex.Message);
            }
        }
    }
}
