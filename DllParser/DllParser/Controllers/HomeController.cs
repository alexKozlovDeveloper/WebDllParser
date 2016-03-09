using DllParser.Configuration;
using DllParser.Core;
using DllParser.Core.Entities;
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
            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        Stream stream = fileContent.InputStream;
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".dll";
                        string path = Path.Combine(Server.MapPath(Keys.AppDataPath), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        LibraryParser libraryParser = new LibraryParser(path);

                        Session[Keys.AssemblyParser] = libraryParser;

                        return Json(libraryParser.Namespaces.Select(a => new NamespaceEntity(a.Key)));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload or parse failed.");
            }

            return Json(new List<string>());
        }

        public JsonResult GetTypeInfo(string name)
        {
            LibraryParser libraryParser = Session[Keys.AssemblyParser] as LibraryParser;

            if (libraryParser == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Not found Types");
            }

            try
            {
                return Json(libraryParser.GetTypeModel(name));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed.");
            }
        }

        public JsonResult GetTypeFromNamespace(string namespaceName)
        {
            LibraryParser libraryParser = Session[Keys.AssemblyParser] as LibraryParser;

            if (libraryParser == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Not found Types");
            }
            
            try
            {
                return Json(libraryParser.Namespaces[namespaceName].Select(a => new ClassEntity(a.Name)).ToList());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed.");
            }
        }
    }
}
