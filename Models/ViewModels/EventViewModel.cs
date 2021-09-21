using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarMVC.Models.ViewModels
{
    public class ViewModel
    {
        public Appointment Appointment { get; set; }
        public List<SelectListItem> Client = new List<SelectListItem>(); //Instantiates list before passing through constructor
        public string ClientName { get; set; }
        public string UserId { get; set; }

        public ViewModel(Appointment appointment, List<Client> clients, string userid) // constructor to be used in Edit View
        {
            Appointment = appointment;
            ClientName = Appointment.Client.Name;
            UserId = userid;
            foreach (var client in clients)
            {
                Client.Add(new SelectListItem() { Text = client.Name });
            }
        }

        public ViewModel(List<Client> clients, string userid) // constructor to be used in Create View
        {
            UserId = userid;
            foreach (var client in clients)
            {
                Client.Add(new SelectListItem() { Text = client.Name });
            }
        }

        public ViewModel()
        {

        }

    }
}
