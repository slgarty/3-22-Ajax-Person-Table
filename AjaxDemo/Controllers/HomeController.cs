using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=MyFirstDb;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var db = new PersonDb(_connectionString);
            db.Add(person);
            return Json(person);
        }

        public IActionResult GetAll()
        {
            var db = new PersonDb(_connectionString);
            List<Person> ppl = db.GetAll();
            return Json(ppl);
        }
        [HttpPost]
        public IActionResult Update(Person person)
        {
            var db = new PersonDb(_connectionString);
            db.Edit(person);
            return Json(person);
        }
        [HttpPost]
        public IActionResult Edit(int id)
        {
            var db = new PersonDb(_connectionString);
            var person = db.GetPersonById(id);
            db.Edit(person);
            return Json(id);
        }
        [HttpPost]
        public IActionResult Delete (int id)
        {
            var db = new PersonDb(_connectionString);
            db.Delete(id);
            return Json(id);
        }
    }

}
