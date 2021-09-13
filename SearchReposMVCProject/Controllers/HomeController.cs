using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchReposMVCProject.Models;
using SearchReposMVCProject.Models.Json;
using SearchReposMVCProject.Src.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ISearchEngine _se;

       
        public HomeController(ILogger<HomeController> logger, ISearchEngine se)
        {
            _se = se;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string? search)
        {
            if (String.IsNullOrEmpty(search) == true) return View();

            //ToDo сделать через ajax
            string json = _se.GetResult(search);
            RepositoryData data = JsonSerializer.Deserialize<RepositoryData>(json);

            ViewBag.SearchString = search;

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
