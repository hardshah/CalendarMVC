using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarMVC.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; } // primary key
        public string Name { get; set; }// column
        public string Description { get; set; }// column
        public DateTime StartTime { get; set; }// column
        public DateTime EndTime { get; set; }// column

        //Relational data
        public virtual Client Client { get; set; }// One appointment to one client (One to One relationship) 
        public virtual User User { get; set; }// One appointment to one user  (One to One relationship)

        public Appointment(IFormCollection form, Client client, User user) // constructor 
        {
            User = user;
            Name = form["Appointment.Name"].ToString();
            Description = form["Appointment.Description"].ToString();
            StartTime = DateTime.Parse(form["Appointment.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Appointment.EndTime"].ToString());
            Client = client;
        }

        public void UpdateAppointment(IFormCollection form, Client client, User user) // Same as constructor
        {
            User = user;
            Name = form["Appointment.Name"].ToString();
            Description = form["Appointment.Description"].ToString();
            StartTime = DateTime.Parse(form["Appointment.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Appointment.EndTime"].ToString());
            Client = client;
        }

        public Appointment() // no parameter
        {

        }
    }
}
