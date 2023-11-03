using CachingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;

namespace CachingDemo.Controllers
{
    public class HomeController : Controller
    {
        IMemoryCache caching = null;


        public HomeController(IMemoryCache cache)
        {
            caching = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            DateTime currentTime;
            if (!caching.TryGetValue<DateTime>("currentTime", out currentTime))
            {
                currentTime = DateTime.Now;
                caching.Set<DateTime>("currentTime", currentTime, DateTimeOffset.Now.AddMinutes(10));
            }


            ViewBag.Time = currentTime;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
