using imgtotext.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Tesseract;

namespace imgtotext.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadModel model)
        {
            if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);
                model.ImageFile.SaveAs(path);

                string transactionId = ExtractTransactionIDFromImage(path);
                ViewBag.ExtractedTransactionID = transactionId;
            }
            return View("Index", model);
        }

        private string ExtractTransactionIDFromImage(string imagePath)
        {
            string tessDataPath = Server.MapPath("~/tessdata");

            using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        string extractedText = page.GetText();
                        return ExtractTransactionID(extractedText);
                    }
                }
            }
        }

        private string ExtractTransactionID(string text)
        {
            var match = Regex.Match(text, @"(?:Transaction ID|ID|UTR)\s*:?[\s]*([\w\d-]+)");           
            return match.Success ? match.Groups[1].Value : "Transaction ID not found.";
        }
    }
}
