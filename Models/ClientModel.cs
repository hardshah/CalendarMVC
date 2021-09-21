using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarMVC.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; } // primary key
        public string Name { get; set; }// column

        //Relational data
        public virtual ICollection<Appointment> Appointments { get; set; } // One client to many appointments (One to many relationship)
    }
}
