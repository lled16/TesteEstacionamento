using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public ParkingSpotType Type { get; set; }
        public bool IsOccupied { get; set; }
        public ParkingSpotType TypeVehicle { get; set; }
        public string LicensePlate { get; set; }
    }
}
