using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Booking.Commands.CreateBooking
{
    public class BookingDto
    {
        public int ParkingSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateBook { get; set; }
        public string? GuestName { get; set; }
        public string? GuestPhone { get; set; }
        public string? PaymentMethod { get; set; }
        public int VehicleInforId { get; set; }
        /// <summary>
        /// UserId will be set from JWT token by the controller, not from client request
        /// </summary>
        public int UserId { get; set; }
    }
}
