// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using talCalenderbooking.Interfaces;
using talCalenderbooking.Services;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace talCalenderbooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Batteries.Init();
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDbConnection>(connection)
                .AddSingleton<IAppointmentService>(provider =>
                new AppointmentService(provider.GetRequiredService<IDbConnection>(), "Data Source=DESKTOP-4OSOAMP;Initial Catalog=CalendarBooking;Persist Security Info=True;Integrated Security=true;"))
                .AddSingleton<CommandLineInterface>()
                .BuildServiceProvider();

            var cli = serviceProvider.GetRequiredService<CommandLineInterface>();
            cli.ParseCommand(args);

            connection.Dispose();
        }
    }
}