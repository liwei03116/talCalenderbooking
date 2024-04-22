using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talCalenderbooking.Services;

namespace calenderBooking.test
{
    public class calenderBookingTest
    {
        private readonly string connectionString = "Data Source=DESKTOP-4OSOAMP;Initial Catalog=CalendarBooking;Persist Security Info=True;Integrated Security=true;";

        [Fact]
        public void AddAppointment_Should_Add_Appointment()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();

            var appointmentService = new AppointmentService(mockDbConnection.Object, connectionString);

            DateTime date = DateTime.Now.Date;
            TimeSpan startTime = TimeSpan.FromHours(9);

            // Act
            mockDbConnection.Setup(c => c.Open()).Verifiable();
            appointmentService.AddAppointment(date, startTime);

            // Assert
            mockDbConnection.Verify(c => c.Open(), Times.Once);

            mockDbConnection.Verify(c => c.Open(), Times.AtMostOnce, "Open method should be called before any database operation.");
        }

        [Fact]
        public void DeleteAppointment_Should_Delete_Appointment()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            mockDbConnection.Setup(c => c.Open()).Verifiable(); 

            var appointmentService = new AppointmentService(mockDbConnection.Object, connectionString);

            DateTime date = DateTime.Now.Date;
            TimeSpan startTime = TimeSpan.FromHours(9); 

            // Act
            appointmentService.DeleteAppointment(date, startTime);

            // Assert
            mockDbConnection.Verify(c => c.Open(), Times.Once); 
        }

        [Fact]
        public void FindFreeTimeSlots_Should_Return_Free_Time_Slots()
        {
            // Arrange
            var appointmentService = new AppointmentService(Mock.Of<IDbConnection>(), connectionString);

            DateTime date = DateTime.Now.Date;

            // Act
            var freeTimeSlots = appointmentService.FindFreeTimeSlots(date);

            // Assert
            Assert.NotNull(freeTimeSlots);
            Assert.False(freeTimeSlots.Any());
        }

        [Fact]
        public void KeepTimeSlot_Should_Keep_Time_Slot()
        {
            // Arrange
            var mockDbConnection = new Mock<IDbConnection>();
            mockDbConnection.Setup(c => c.Open()).Verifiable(); 

            var appointmentService = new AppointmentService(mockDbConnection.Object, "your_connection_string_here");

            TimeSpan startTime = TimeSpan.FromHours(9); 

            // Act
            appointmentService.KeepTimeSlot(startTime);

            // Assert
            mockDbConnection.Verify(c => c.Open(), Times.Once); 
        }
    }
}
