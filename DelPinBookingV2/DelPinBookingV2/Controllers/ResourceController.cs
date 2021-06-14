using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DelPinBookingV2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace DelPinBookingV2.Controllers
{
    [Authorize]

    public class ResourceController : Controller
    {
        string url = "https://delpinresourceapi.azurewebsites.net/api/";
        

        IEnumerable<SubCategory> categories;
        public ResourceController()
        {
           
            HttpResponseMessage result = NewClient("SubCategories").GetAsync("").Result;
            categories = result.Content.ReadAsAsync<IEnumerable<SubCategory>>().Result;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCalendarResources()
        {
            HttpResponseMessage response = NewClient("Resources").GetAsync("").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IEnumerable<Resource>>().Result;

                foreach (var item in data)
                {
                    item.groupId = categories.Where(s => s.Id == item.SubCategoryId).FirstOrDefault().SubCategoryName;
                }
                return Json(data);
            }
            else
                return null;
        }

        public HttpClient NewClient(string action)
        {
            var returnClient = new HttpClient { BaseAddress = new Uri(url+action) };
            returnClient.DefaultRequestHeaders.Accept.Clear();
            returnClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return returnClient;

        }

    }
}
