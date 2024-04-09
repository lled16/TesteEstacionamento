using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ParkingService : IParkingService
    {
        static List<ParkingSpot> parkingSpot = SeedSpots.SeedSpotsParking();
        const int numberSpotsVansOcupiedInCarSpots = 3;

        public int GetEmptySpots()
        {
            return parkingSpot.Where(p => p.IsOccupied == false).Count();
        }
        public int GetTotalSpots()
        {
            return parkingSpot.Count();
        }
        public bool VerifyFullParkingLot()
        {
            foreach (var parking in parkingSpot)
            {
                if (!parking.IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }
        public bool VerifyEmptyParkingLot()
        {
            foreach (var parking in parkingSpot)
            {
                if (parking.IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        public bool VerifyFullGroupType(ParkingSpotType parkingSpotType)
        {
            return parkingSpot.Where(p => p.Type == parkingSpotType).All(p => p.IsOccupied == true);
        }

        public int VerifyAmountOfVans()
        {
            int countAmopuntVansInBigSpots = parkingSpot.Where(p => p.IsOccupied is true &&
                                                               p.Type == ParkingSpotType.BigSpot &&
                                                               p.TypeVehicle == ParkingSpotType.BigSpot).Count();

            int countAmountVansInCarSpots = parkingSpot.Where(p => p.IsOccupied is true &&
                                                               p.Type == ParkingSpotType.Car &&
                                                               p.TypeVehicle == ParkingSpotType.BigSpot)
                                                               .Count() / numberSpotsVansOcupiedInCarSpots;

            return countAmopuntVansInBigSpots + countAmountVansInCarSpots;

        }


        public bool Park(ParkingSpotType parkingSpotType, string licensePlate)
        {
            int countEmptySpotsBike = parkingSpot.Where(p => p.IsOccupied is false &&
                                                               p.Type == ParkingSpotType.Bike).Count();

            int countEmptySpotsCar = parkingSpot.Where(p => p.IsOccupied is false &&
                                                               p.Type == ParkingSpotType.Car).Count();

            int countEmptySpotsBigSpot = parkingSpot.Where(p => p.IsOccupied is false &&
                                                               p.Type == ParkingSpotType.BigSpot).Count();

            int positionFree = 0;

            if (parkingSpotType == ParkingSpotType.Bike)
            {
                if (countEmptySpotsBike > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.Bike).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
                else if (countEmptySpotsCar > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
                else if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
            }
            else if (parkingSpotType == ParkingSpotType.Car)
            {
                if (countEmptySpotsCar > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
                else if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
            }
            else if (parkingSpotType == ParkingSpotType.BigSpot)
            {
                if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    return true;
                }
                else if (countEmptySpotsCar >= numberSpotsVansOcupiedInCarSpots)
                {
                    positionFree = parkingSpot.FirstOrDefault(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    for (int i = 0; i < numberSpotsVansOcupiedInCarSpots; i++)
                    {
                        parkingSpot[positionFree + i].IsOccupied = true;
                        parkingSpot[positionFree + i].TypeVehicle = parkingSpotType;
                        parkingSpot[positionFree + i].LicensePlate = licensePlate;

                    }

                    return true;
                }
            }

            return false;
        }

        public bool RemovePark(string licensePlate)
        {
            try
            {
                int positionPark = 0;

                positionPark = parkingSpot.FirstOrDefault(p => p.IsOccupied is true && p.LicensePlate == licensePlate).Id;

                ParkingSpot parkingSpotUpdate = new ParkingSpot()
                {
                    Id = positionPark,
                    IsOccupied = false
                };

                if (positionPark <= 6)
                {
                    parkingSpotUpdate.Type = ParkingSpotType.Bike;
                }
                else if (positionPark > 6 && positionPark <= 13)
                {
                    parkingSpotUpdate.Type = ParkingSpotType.Car;
                }
                else if (positionPark > 13 && positionPark < 21)
                {
                    parkingSpotUpdate.Type = ParkingSpotType.BigSpot;
                }

                parkingSpot[positionPark] = parkingSpotUpdate;

                return true;
            }
            catch
            {
                return false;
            }


        }

    }
}
