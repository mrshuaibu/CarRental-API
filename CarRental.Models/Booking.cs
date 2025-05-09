using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public int CarId { get; set; }

        public Car? Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}
