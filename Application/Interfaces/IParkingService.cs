using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IParkingService
    {
        int GetTotalSpots();
        public int GetEmptySpots();
        public bool VerifyFullParkingLot();
        public bool VerifyEmptyParkingLot();
        public bool VerifyFullGroupType(ParkingSpotType parkingSpotType);
        public int VerifyAmountOfVans();
        bool Park(ParkingSpotType parkingSpotType, string licensePlate);
        bool RemovePark(string licensePlate);
    }
}
