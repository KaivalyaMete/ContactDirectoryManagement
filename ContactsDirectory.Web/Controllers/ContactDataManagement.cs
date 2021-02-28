using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContactsDirectory.Web.Controllers
{
    public class ContactDataManagement : Controller
    {
        public async Task<IActionResult> ListAllContacts()
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
        private static async Task<ContactInformation> GetContactById(int Id)
        {
            ContactInformation contactInfo = new ContactInformation();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64053/");
            HttpResponseMessage response = await client.GetAsync($"api/ContactManagement/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                contactInfo = JsonConvert.DeserializeObject<ContactInformation>(results);
            }

            return contactInfo;
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactInformation contactInformation)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64053/");
            var response = await client.PostAsJsonAsync<ContactInformation>("api/ContactManagement", contactInformation);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListAllContacts");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditContact(int Id)
        {
            ContactInformation contactId = await GetContactById(Id);
            return View(contactId);
        }

        [HttpPost]
        public async Task<IActionResult> EditContact(ContactInformation contactInformation)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64053/");
            var response = await client.PutAsJsonAsync<ContactInformation>($"api/ContactManagement/{contactInformation.Id}", contactInformation);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListAllContacts");
            }
            return View();
        }

        public async Task<IActionResult> DeleteContact(int Id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64053/");
            HttpResponseMessage response = await client.DeleteAsync($"api/ContactManagement/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListAllContacts");
            }
            return View();
        }
    }
}
