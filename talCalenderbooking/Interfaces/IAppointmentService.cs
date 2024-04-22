using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talCalenderbooking.Models;

namespace talCalenderbooking.Interfaces
{
    public interface IAppointmentService
    {
        void AddAppointment(DateTime date, TimeSpan startTime);
        void DeleteAppointment(DateTime date, TimeSpan startTime);
        IEnumerable<Appointment> FindFreeTimeSlots(DateTime date);
        void KeepTimeSlot(TimeSpan startTime);
    }
}
