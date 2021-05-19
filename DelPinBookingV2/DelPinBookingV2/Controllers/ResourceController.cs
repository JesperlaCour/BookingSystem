using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DelPinBooking.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using ResourceAPI.Models;

namespace DelPinBookingV2.Controllers
{
    public class ResourceController : Controller
    {
        HttpClient client;
        string url = "https://localhost:5002/api/Resources";


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCalendarResources()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IEnumerable<Resource>>().Result;
                return Json(data);
            }
            else
                return null;
        }

    }
}
