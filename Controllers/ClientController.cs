using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalendarMVC.Data;
using CalendarMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CalendarMVC.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IDAL _dal;
        private readonly UserManager<User> _usermanager;

        public ClientController(IDAL idal, UserManager<User> usermanager) 
        {
            _dal = idal;  // dependency injection
            _usermanager = usermanager; // dependency injection
           
        }

        // GET: Client
        public IActionResult Index()// Displays list of clients
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_dal.GetClients());
        }

        // GET: Client/Details/5
        public IActionResult Details(int? id)// Displays client details 
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _dal.GetClient((int)id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create() // Returns client create view
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Client client) // Creates client

        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dal.CreateClient(client);
                    TempData["Alert"] = "Success! You created a client: " + client.Name;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewData["Alert"] = "An error occurred: " + ex.Message;
                    return View(client);
                }
                
            }
            return View(client);
        }
    }
}
