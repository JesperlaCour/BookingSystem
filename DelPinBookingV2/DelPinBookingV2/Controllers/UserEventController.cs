using DelPinBookingV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DelPinBookingV2.Controllers
{
    public class UserEventController : Controller
    {
        HttpClient client;
        string url = "https://delpineventapi.azurewebsites.net/api/events";

        // GET: UserEventController
        public ActionResult Index()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;


            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<List<Event>>().Result;
                data = data.Where(s => s.UserName.Contains(User.Identity.Name)).ToList();

                return View(data);
            }
            else
                return View();
        }

        // GET: UserEventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: UserEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event e)
        {
            if (ModelState.IsValid)
            {

                client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.PostAsJsonAsync<Event>($"Events", e);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest();
            }
            return BadRequest();
        }

        // GET: UserEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserEventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
