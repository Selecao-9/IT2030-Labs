using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{

    //[HttpsOnly]
    [ResultDiagnostics]
    //[GuidResponse]
    //[GuidResponse]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View("Message",
                "This is the Index action on the Home controller");
        }
    }
}

