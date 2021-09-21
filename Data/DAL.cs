using CalendarMVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace CalendarMVC.Data
{
    public interface IDAL
    {
        public List<Appointment> GetAppointments();
        public List<Appointment> GetMyAppointments(string userid);
        public Appointment GetAppointment(int id);
        public void CreateAppointment(IFormCollection form);
        public void UpdateAppointment(IFormCollection form);
        public void DeleteAppointment(int id);
        public List<Client> GetClients();
        public Client GetClient(int id);
        public void CreateClient(Client client);
    }

    public class DAL : IDAL // inherits and creates seperation of concerns
    {
        private ApplicationDbContext db = new ApplicationDbContext();// Instantiate before using

        public List<Appointment> GetAppointments()
        {
            return db.Appointments.ToList();
        }

        public List<Appointment> GetMyAppointments(string userid)
        {
            return db.Appointments.Where(p => p.User.Id == userid).ToList();
        }

        public Appointment GetAppointment(int id)
        {
            return db.Appointments.FirstOrDefault(p => p.Id == id);
        }

        public void CreateAppointment(IFormCollection form)
        {
            var clientname = form["Client"].ToString();
            var user = db.Users.FirstOrDefault(p => p.Id == form["UserId"].ToString());
            var newAppointment = new Appointment(form, db.Clients.FirstOrDefault(p => p.Name == clientname), user);
            db.Appointments.Add(newAppointment);
            db.SaveChanges();
        }

        public void UpdateAppointment(IFormCollection form)
        {
            var clientname = form["Location"].ToString();
            var Appointmentid = int.Parse(form["Appointment.Id"]);
            var myAppointment = db.Appointments.FirstOrDefault(p => p.Id == Appointmentid);
            var location = db.Clients.FirstOrDefault(p => p.Name == clientname);
            var user = db.Users.FirstOrDefault(p => p.Id == form["UserId"].ToString());
            myAppointment.UpdateAppointment(form, location, user);
            db.Entry(myAppointment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteAppointment(int id)
        {
            var myAppointment = db.Appointments.FirstOrDefault(p=> p.Id == id);
            db.Appointments.Remove(myAppointment);
            db.SaveChanges();
        }

        public List<Client> GetClients() => db.Clients.ToList();

        public Client GetClient(int id)
        {
            return db.Clients.FirstOrDefault(p => p.Id == id);
        }

        public void CreateClient(Client Client)
        {
            db.Clients.Add(Client);
            db.SaveChanges();
        }
    }
}
