using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models.GU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Controllers
{
    public class MarketController : Controller
    {
        IGU2VRepository repo;
        public MarketController(IGU2VRepository r)
        {
            repo = r;
        }
       
        public ActionResult Dislokacia()
        {
            return View(repo.GetDislokacia());
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var dislokacias = await repo.GetDislokacias();
                return Ok(dislokacias);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }

    }
}
