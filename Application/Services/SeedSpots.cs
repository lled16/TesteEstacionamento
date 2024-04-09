using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeedSpots
    {
        public static List<ParkingSpot> SeedSpotsParking()
        {
            const int qtMaxSpots = 21;
            ParkingSpot[] parkingSpots = new ParkingSpot[qtMaxSpots];

            for (int i = 0; i < qtMaxSpots; i++)
            {
                ParkingSpot parkingSpot = new ParkingSpot()
                {
                    Id = i,
                    IsOccupied = false
                };

                if (i <= 6)
                {
                    parkingSpot.Type = ParkingSpotType.Bike;
                }
                else if (i > 6 && i <= 13)
                {
                    parkingSpot.Type = ParkingSpotType.Car;
                }
                else if (i > 13 && i < qtMaxSpots)
                {
                    parkingSpot.Type = ParkingSpotType.BigSpot;
                }

                parkingSpots[i] = parkingSpot;
            }

            return parkingSpots.ToList();
        }
    }
}
