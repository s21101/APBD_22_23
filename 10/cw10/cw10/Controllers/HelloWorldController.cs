using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace cw10.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //return "This is my default action...";
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
            //return "This is the Welcomde action method...";
        }
    }
}
