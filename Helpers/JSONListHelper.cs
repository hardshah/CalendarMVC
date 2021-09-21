using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarMVC.Helpers
{
    public static class JSONListHelper
    {
        public static string GetEventListJSONString(List<Models.Appointment> appointments)// Returns Serialized JSON String of appointments so FullCalendar.io can correctly interpret
        {
            var appointmentlist = new List<AppointmentFullCalendar>();
            foreach (var model in appointments)
            {
                var myAppointment = new AppointmentFullCalendar()
                {
                    id = model.Id,
                    start = model.StartTime,
                    end = model.EndTime,
                    resourceId = model.Client.Id,
                    description = model.Description,
                    title = model.Name
                };
                appointmentlist.Add(myAppointment); // Getting list of Appointments and turning into list of AppointmentFullCalendar

            }
            return System.Text.Json.JsonSerializer.Serialize(appointmentlist);
        }

        public static string GetResourceListJSONString(List<Models.Client> clients)// returns Serialized JSON string of Clients that is interpreted as a resource with a resource id 
        {
            var resourcelist = new List<Resource>();

            foreach (var client in clients)
            {
                var resource = new Resource()
                {
                    id = client.Id,
                    title = client.Name
                };
                resourcelist.Add(resource); // Getting list of Clients and turning into list of Resources
            }
            return System.Text.Json.JsonSerializer.Serialize(resourcelist);
        }
    }

    public class AppointmentFullCalendar // different in the context of FullCalendar.io
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; } // string id of resource corresponds to a client 
        public string description { get; set; }
    }

    public class Resource // Client class will be interpreted as Resource in Serialized JSON string
    {
        public int id { get; set; }
        public string title { get; set; }

    }
}
