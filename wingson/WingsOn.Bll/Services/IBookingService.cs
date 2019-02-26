using System.Collections.Generic;
using WingsOn.Bll.Models;
using WingsOn.Domain;

namespace WingsOn.Bll.Services
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetBookings();
        Booking GetBooking(int id);
        Booking CreateBooking(CreateBooking createBooking);
    }
}