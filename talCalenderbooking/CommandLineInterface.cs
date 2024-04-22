using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talCalenderbooking.Interfaces;

namespace talCalenderbooking
{
    public class CommandLineInterface
    {
        private readonly IAppointmentService _appointmentService;

        public CommandLineInterface(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public void ParseCommand(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Insufficient arguments. Please provide command and required parameters.");
                return;
            }

            string command = args[0].ToLower();
            DateTime date;
            TimeSpan startTime;

            if (!DateTime.TryParse(args[1], out date))
            {
                Console.WriteLine("Invalid date format. Please provide the date in yyyy-MM-dd format.");
                return;
            }

            if (args.Length == 3 && !TimeSpan.TryParse(args[2], out startTime))
            {
                Console.WriteLine("Invalid time format. Please provide the time in HH:mm format.");
                return;
            }

            date = DateTime.Parse(args[1]);
            startTime = TimeSpan.Parse(args[2]);

            switch (command)
            {
                case "add":
                    if (args.Length != 3)
                    {
                        Console.WriteLine("Invalid number of arguments for 'add' command. Please provide date and time.");
                        return;
                    }
                    _appointmentService.AddAppointment(date, startTime);
                    break;

                case "delete":
                    if (args.Length != 3)
                    {
                        Console.WriteLine("Invalid number of arguments for 'delete' command. Please provide date and time.");
                        return;
                    }
                    _appointmentService.DeleteAppointment(date, startTime);
                    break;

                case "find":
                    if (args.Length != 2)
                    {
                        Console.WriteLine("Invalid number of arguments for 'find' command. Please provide date only.");
                        return;
                    }
                    var freeTimeSlots = _appointmentService.FindFreeTimeSlots(date);
                    break;

                case "keep":
                    if (args.Length != 3)
                    {
                        Console.WriteLine("Invalid number of arguments for 'keep' command. Please provide time only.");
                        return;
                    }
                    _appointmentService.KeepTimeSlot(startTime);
                    break;

                default:
                    Console.WriteLine("Invalid command. Supported commands: add, delete, find, keep.");
                    break;
            }
        }
    }
}
