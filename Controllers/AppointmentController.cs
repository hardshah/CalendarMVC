using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalendarMVC.Data;
using CalendarMVC.Models;
using CalendarMVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CalendarMVC.Controllers.ActionFilters;

namespace CalendarMVC.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IDAL _dal;
        private readonly UserManager<User> _usermanager;

        public AppointmentController(IDAL dal, UserManager<User> usermanager)
        {
            _dal = dal; // dependency injection
            _usermanager = usermanager;
        }

        // GET: Appointment
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"]; // View["Alert"] displayed 
            }
            return View(_dal.GetMyAppointments(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        // GET: Appointment/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @Appointment = _dal.GetAppointment((int)id);
            if (@Appointment == null)
            {
                return NotFound();
            }

            return View(@Appointment);// returns corresponding appointment's View
        }

        // GET: Appointment/Create
        
        public IActionResult Create()
        {
            return View(new ViewModel(_dal.GetClients(), User.FindFirstValue(ClaimTypes.NameIdentifier)));//returns Create Page with ability to have dropdown list
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(ViewModel viewmodel, IFormCollection form)//using form data to create appointment
        {
            try
            {
                _dal.CreateAppointment(form);
                TempData["Alert"] = "Success! You created a new appointment for: " + form["Appointment.Name"];
                return RedirectToAction("Index");
            } catch(Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                return View(viewmodel);
            }
        }

        // GET: Appointment/Edit/5
        [UserAccessOnly]
        public IActionResult Edit(int? id)// returns Edit View for appointment
        {
            if (id == null)
            {
                return NotFound();
            }

            var @Appointment = _dal.GetAppointment((int)id);
            if (@Appointment == null)
            {
                return NotFound();
            }
            var viewmodel = new ViewModel(@Appointment, _dal.GetClients(), User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(viewmodel);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Edit(int id, IFormCollection form)// modifies appointment data
        {
            try
            {
                _dal.UpdateAppointment(form);
                TempData["Alert"] = "Success! You modified an appointment for: " + form["Appointment.Name"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                var viewmodel = new ViewModel(_dal.GetAppointment(id), _dal.GetClients(), User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(viewmodel);
            }
        }

        // GET: Appointment/Delete/5
        public IActionResult Delete(int? id)//returns Delete Page
        {
            if (id == null)
            {
                return NotFound();
            }
            var @Appointment = _dal.GetAppointment((int)id);
            if (@Appointment == null)
            {
                return NotFound();
            }

            return View(@Appointment);
        }

        // POST: Appointment/Delete/5// Deletes appointment 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _dal.DeleteAppointment(id);
            TempData["Alert"] = "You deleted an appointment.";
            return RedirectToAction(nameof(Index));
        }
    }
}
