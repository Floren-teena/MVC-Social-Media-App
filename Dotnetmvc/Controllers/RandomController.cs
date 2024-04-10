using dotnetcoremorningclass.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoremorningclass.Controllers
{
    public class RandomController : Controller
    {
        public IActionResult Index()
        {
            var movies = new Movies() { Name = "The Actual Booming" };
            return View(movies);
        }

    }
}
