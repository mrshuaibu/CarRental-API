using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class CreateCarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string LicensePlate { get; set; }

        public string Status { get; set; }

        public int RentalPricePerDay { get; set; }
    }
}
