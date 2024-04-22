using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talCalenderbooking.Interfaces;
using talCalenderbooking.Models;

namespace talCalenderbooking.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDbConnection _dbConnection;
        private readonly string _connectionString;

        public AppointmentService(IDbConnection dbConnection, string connectionString)
        {
            _dbConnection = dbConnection;
            _connectionString = connectionString;
        }

        public void AddAppointment(DateTime date, TimeSpan startTime)
        {
            try
            {
                TimeSpan endTime = startTime.Add(TimeSpan.FromMinutes(30));

                if(_dbConnection is SqlConnection sqlConnection)
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        _dbConnection.Open();

                        command.CommandText = "INSERT INTO Appointments (Date, StartTime, EndTime) VALUES (@Date, @StartTime, @EndTime)";
                        command.Parameters.AddWithValue("@Date", date.Date);
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@EndTime", endTime);

                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unsupported database connection type.");
                }

                Console.WriteLine("Appointment added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding appointment: {ex.Message}");
            }
        }

        public void DeleteAppointment(DateTime date, TimeSpan startTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Appointments WHERE Date = @Date AND StartTime = @StartTime";
                        command.Parameters.AddWithValue("@Date", date.Date);
                        command.Parameters.AddWithValue("@StartTime", startTime);

                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
            }
        }

        public IEnumerable<Appointment> FindFreeTimeSlots(DateTime date)
        {
            try
            {
                List<Appointment> freeTimeSlots = new List<Appointment>();

                TimeSpan acceptableStartTime = TimeSpan.FromHours(9);
                TimeSpan acceptableEndTime = TimeSpan.FromHours(17);

                return freeTimeSlots;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding free time slots: {ex.Message}");
                return Enumerable.Empty<Appointment>();
            }
        }

        public void KeepTimeSlot(TimeSpan startTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Appointments SET IsReserved = 1 WHERE Date = @Date AND StartTime = @StartTime";
                        command.Parameters.AddWithValue("@Date", DateTime.Today.Date);
                        command.Parameters.AddWithValue("@StartTime", startTime);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Time slot kept successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No appointment found for the specified time slot.");
                        }
                    }
                }
            }
            catch (Exception ex)
    {
        Console.WriteLine($"Error keeping time slot: {ex.Message}");
    }
        }
    }
}
