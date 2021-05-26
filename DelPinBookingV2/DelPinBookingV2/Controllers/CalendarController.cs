using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using DelPinBookingV2.Models;
using System.Net.Http;
using System.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DelPinBooking.Controllers
{

    [Authorize]
    public class CalendarController : Controller
    {
        HttpClient client;
        string url = "https://localhost:5001/api/Events";


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCalendarEvents()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                return Json(data);
            }
            else
                return null;
        }

        [HttpGet]
        public JsonResult GetEvent(string id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"Events/"+ id).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<Event>().Result;
                return Json(data);
            }
            else
                return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEvent(Event e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var postTask = client.PutAsJsonAsync<Event>($"Events/" + e.Id, e);
            postTask.Wait();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(Event e)
        {
            if (ModelState.IsValid)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var postTask = client.PostAsJsonAsync<Event>($"Events", e);
                postTask.Wait();
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int? id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var postTask = client.DeleteAsync($"Events/" + id);
            postTask.Wait();
            return RedirectToAction("Index");
        }
    }
}
