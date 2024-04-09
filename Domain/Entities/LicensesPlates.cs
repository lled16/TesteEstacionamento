using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LicensesPlates
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string TypeVehicle { get; set; }
    }
}
