using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarMVC.Models
{
    public class User : IdentityUser // inherits from IdentityUser

        //Relational Data
    {
        public virtual ICollection<Appointment> Appointments { get; set; } // One user to many appointments (One to Many relationship)
    }
}
 