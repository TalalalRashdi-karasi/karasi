using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Context;

namespace Shubak_Website.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly QRCodeService _qrCodeService;

        public QRCodeController()
        {
            _qrCodeService = new QRCodeService();
        }

        [HttpPost]
        public ActionResult Generate(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "URL cannot be empty");
            }

            var qrCodeImage = _qrCodeService.GenerateQRCode(url);
            return File(qrCodeImage, "image/png");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;
        private string v;

        public HttpStatusCodeResult(HttpStatusCode badRequest, string v)
        {
            this.badRequest = badRequest;
            this.v = v;
        }
    }
}
