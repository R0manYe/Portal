using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models.GU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    public class GU2VController : Controller
    {
       
            IGU2VRepository repo;
            public GU2VController(IGU2VRepository r)
            {
                repo = r;
            }
        [Authorize(Roles = "GU2V")]
        public ActionResult GU2V()
            {
                return View(repo.GetGU2V());
            }
            



        }
    }
