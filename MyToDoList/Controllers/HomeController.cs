using Microsoft.AspNetCore.Mvc;
using MyToDoList.Models;
using System.Diagnostics;

namespace MyToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Data.MyConnection connection = new Data.MyConnection();
            string task = HttpContext.Request.Query["task"];
            int.TryParse(HttpContext.Request.Query["on"], out int on);
            int.TryParse(HttpContext.Request.Query["off"], out int off);
            int.TryParse(HttpContext.Request.Query["delete"], out int delete);
            if (checkQueryParameters(on))
            {
                var changeTask = connection.tasks.Where(t => t.Id == on).First();
                changeTask.status = false;
                connection.SaveChanges();

            }
            if (checkQueryParameters(off))
            {
                var changeTask = connection.tasks.Where(t => t.Id == off).First();
                changeTask.status = true;
                connection.SaveChanges();

            }
            if (checkQueryParameters(delete))
            {
                connection.tasks.Remove(connection.tasks.Where(t => t.Id == delete).First());
                connection.SaveChanges();
            }
            if (task != null && task != "")
            {
                ViewBag.contains = false;
                for (int i = 0; i < connection.tasks.ToList().Count; i++)
                {
                    if (connection.tasks.ToList()[i].Title == task)
                    {
                        ViewBag.contains = true;
                        break;
                    }
                }
                if (ViewBag.contains == false)
                {
                    connection.tasks.Add(new Models.task { Title = task });
                    connection.SaveChanges();
                }
                
            }
            ViewBag.tasks = connection.tasks.ToList();
            ViewBag.dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            ViewBag.date = DateTime.Now.ToShortDateString().ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public bool checkQueryParameters(int index)
        {
            Data.MyConnection connection = new Data.MyConnection();
            for (int i = 0; i < connection.tasks.ToList().Count; i++)
            {
                if (connection.tasks.ToList()[i].Id == index)
                {
                    return true;
                }
            }
            return false;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}