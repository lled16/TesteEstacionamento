using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System.Text.RegularExpressions;


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
                    return false;
            }
            return true;
        }
        public bool VerifyEmptyParkingLot()
        {
            foreach (var parking in parkingSpot)
            {
                if (parking.IsOccupied)
                    return false;
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
                                                               p.Type is ParkingSpotType.BigSpot &&
                                                               p.TypeVehicle is ParkingSpotType.BigSpot).Count();

            int countAmountVansInCarSpots = parkingSpot.Where(p => p.IsOccupied is true &&
                                                               p.Type is ParkingSpotType.Car &&
                                                               p.TypeVehicle is ParkingSpotType.BigSpot)
                                                               .Count() / numberSpotsVansOcupiedInCarSpots;

            return countAmopuntVansInBigSpots + countAmountVansInCarSpots;

        }

        public ParkResult Park(ParkingSpotType parkingSpotType, string licensePlate)
        {
            ParkResult parkResult = new();

            int countDuplicatedLicensePlate = parkingSpot.Where(p => p.IsOccupied && p.LicensePlate == licensePlate).Count();

            if (countDuplicatedLicensePlate > 0)
            {
                parkResult.Parked = false;
                parkResult.Message = "Veículo já se encontra estacionado !";

                return parkResult;
            }

            int countEmptySpotsBike = parkingSpot.Where(p => p.IsOccupied is false && p.Type is ParkingSpotType.Bike).Count();

            int countEmptySpotsCar = parkingSpot.Where(p => p.IsOccupied is false && p.Type is ParkingSpotType.Car).Count();

            int countEmptySpotsBigSpot = parkingSpot.Where(p => p.IsOccupied is false && p.Type is ParkingSpotType.BigSpot).Count();

            int positionFree = 0;

            if (parkingSpotType is ParkingSpotType.Bike)
            {
                if (countEmptySpotsBike > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.Bike).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Moto estacionada com sucesso em uma vaga de moto !";

                    return parkResult;
                }
                else if (countEmptySpotsCar > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Moto estacionada com sucesso em uma vaga de carro !";

                    return parkResult;
                }
                else if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Moto estacionada com sucesso em uma vaga grande ou de van !";
                }
                else
                {
                    parkResult.Parked = false;
                    parkResult.Message = "Não há vagas de motos, carros ou grandes disponíveis para estacionar !";
                }
            }
            else if (parkingSpotType is ParkingSpotType.Car)
            {
                if (countEmptySpotsCar > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Carro estacionado com sucesso em uma vaga de carro !";
                }
                else if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Carro estacionado com sucesso em uma vaga grande ou de van !";
                }
                else
                {
                    parkResult.Parked = false;
                    parkResult.Message = "Não há vagas de carro ou grandes disponíveis para estacionar !";
                }
            }
            else if (parkingSpotType is ParkingSpotType.BigSpot)
            {
                if (countEmptySpotsBigSpot > 0)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.BigSpot).Id;

                    parkingSpot[positionFree].IsOccupied = true;
                    parkingSpot[positionFree].TypeVehicle = parkingSpotType;
                    parkingSpot[positionFree].LicensePlate = licensePlate;

                    parkResult.Parked = true;
                    parkResult.Message = "Van estacionado com sucesso em uma vaga grande ou de van !";
                }
                else if (countEmptySpotsCar >= numberSpotsVansOcupiedInCarSpots)
                {
                    positionFree = parkingSpot.First(p => p.IsOccupied is false && p.Type == ParkingSpotType.Car).Id;

                    for (int i = 0; i < numberSpotsVansOcupiedInCarSpots; i++)
                    {
                        parkingSpot[positionFree + i].IsOccupied = true;
                        parkingSpot[positionFree + i].TypeVehicle = parkingSpotType;
                        parkingSpot[positionFree + i].LicensePlate = licensePlate;

                    }

                    parkResult.Parked = true;
                    parkResult.Message = "Van estacionado com sucesso ocupando 3 vagas de carro !";
                }
                else
                {
                    parkResult.Parked = false;
                    parkResult.Message = "Não há vagas grandes ou 3 vagas de carro disponíveis para estacionar !";
                }
            }

            return parkResult;
        }

        public RemovePark RemovePark(string licensePlate)
        {
            try
            {
                int positionPark = 0;

                positionPark = parkingSpot.First(p => p.IsOccupied is true && p.LicensePlate == licensePlate).Id;

                parkingSpot[positionPark].IsOccupied = false;
                parkingSpot[positionPark].LicensePlate = null;

                return new RemovePark() { Removed = true, LicensePlate = licensePlate, Message = "Veículo retirado com sucesso !" };
            }
            catch
            {
                return new RemovePark() { Removed = false, LicensePlate = licensePlate, Message = "Veículo não retirado, verifique a placa inserida. Em caso de dúvidas, verifique as placas estacionadas pela rota : VerificarPlacasEstacionadas !" };
            }
        }

        public string ClearPark()
        {
            Parallel.ForEach(parkingSpot, parkingSpot =>
            {
                parkingSpot.IsOccupied = false;
                parkingSpot.LicensePlate = null;
            });

            return  "Estacionamento limpo  com sucesso !";
        }

        public List<LicensesPlates> ReturnLicensesPlates()
        {
            var licensesDistinct = parkingSpot.Where(p => p.IsOccupied).DistinctBy(x => x.LicensePlate).ToList();

            List<LicensesPlates> licensesPlates = new();

            Parallel.ForEach(licensesDistinct, licenses =>
            {
                LicensesPlates plates = new LicensesPlates() { Id = licenses.Id, LicensePlate = licenses.LicensePlate };
                licensesPlates.Add(plates);
            });

            return licensesPlates;
        }
        public  bool ValidLicensePlate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate)) { return false; }

            if (licensePlate.Length > 8) { return false; }

            licensePlate = licensePlate.Replace("-", "").Trim();

           
            if (char.IsLetter(licensePlate, 4))
            {
               
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(licensePlate);
            }
            else
            {
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(licensePlate);
            }
        }
    }
}
