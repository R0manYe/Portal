using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "у вас нет доступа к этому разделу!";
                    break;
                case 403:
                    ViewBag.ErrorMessage = "у вас нет доступа к этому разделу!";
                    break;
            }
            return View("Error");

            
        }
    }
}
