using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactsDirectory.Web.Models;
using DataAccessLayer;
using System.Net.Http;
using Newtonsoft.Json;

namespace ContactsDirectory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ContactInformation> allContactsList = new List<ContactInformation>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64053/");
            HttpResponseMessage response = await client.GetAsync("api/ContactManagement");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                allContactsList = JsonConvert.DeserializeObject<List<ContactInformation>>(results);
            }
            return View(allContactsList);
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
