using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace track_app_admin.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IConfiguration Configuration;

        public UploadFilesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost]
        public JsonResult UploadSercommInventoryEntryFile(IFormFile file)
        {
            long size =  file.Length;

            // full path to file in temp location
            var filePath = Configuration["InventoryEntryReservationFilesPath"] + "//" + file.FileName;

            if (!Directory.Exists(Configuration["InventoryEntryReservationFilesPath"]))
            {
                Directory.CreateDirectory(Configuration["InventoryEntryReservationFilesPath"]);
            }

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Json(new { size, filePath });
        }

        [HttpGet]
        public JsonResult Test()
        {
            string test = "test";
            return Json(new { test });
        }
    }
}
