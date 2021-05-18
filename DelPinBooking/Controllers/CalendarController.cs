using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DelPinBooking.Models;
using System.Net.Http;
using System.Net.Http.Headers;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DelPinBooking.Controllers
{
    public class CalendarController : Controller
    {
        HttpClient client;
        string url = "https://localhost:5001/api/Events";


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

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

            //var result = await client.GetFromJsonAsync<Event>(url);
            //return result;


            //var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //string jsonString = JsonSerializer.ses(responseData);
            //Event[] events = new Event[1];
            //events[0] = new Event
            //{
            //    Id = 1,
            //    ResourceId = 1,
            //    Title = "John",
            //    AllDay = false,
            //    Start = "2021-05-13T12:30:00",
            //    End = "2021-05-13T14:30:00"
            //};
            //return Json(events);
        }

        [HttpPut]
        public ActionResult UpdateEvent(Event e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var postTask = client.PutAsJsonAsync<Event>($"Events/"+e.Id, e);
            postTask.Wait();
            return Json(e.Id);
        }

    }
}
