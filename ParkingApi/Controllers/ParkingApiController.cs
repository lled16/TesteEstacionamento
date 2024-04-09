using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ParkingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;
        private readonly IParkingService _parkingService;
        public ParkingController(ILogger<ParkingController> logger, IParkingService parkingService)
        {
            _logger = logger;
            _parkingService = parkingService;
        }

        [HttpGet("ObterNumeroVagasVazias", Name = "GetEmptySpots")]
        public IActionResult GetEmptySpots()
        {
            return Ok(_parkingService.GetEmptySpots());
        }

        [HttpGet("ObterNumeroDeVagasTotal", Name = "GetTotalVacancy")]
        public IActionResult GetTotalSpots()
        {
            return Ok(_parkingService.GetTotalSpots());
        }

        [HttpGet("VerificarEstacionamentoCheio", Name = "VerifyFullParkingLot")]
        public IActionResult VerifyFullParkingLot()
        {
            return Ok(_parkingService.VerifyFullParkingLot());
        }

        [HttpGet("VerificarEstacionamentoVazio", Name = "VerifyEmptyParkingLot")]
        public IActionResult VerifyEmptyParkingLot()
        {
            return Ok(_parkingService.VerifyEmptyParkingLot());
        }

        /// <summary>
        /// 0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES
        /// </summary>
        /// <param name="tipoVaga"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES</b>
        /// </remarks>
        [HttpGet("VerificarVagasCheiasPorTipo", Name = "VerifyFullGroupType")]
        public IActionResult VerifyFullGroupType(ParkingSpotType tipoVaga)
        {
            return Ok(_parkingService.VerifyFullGroupType(tipoVaga));
        }

        [HttpGet("VerificarQuantidaVagasVansOcupam", Name = "VerifyAmountOfVans")]
        public IActionResult VerifyAmountOfVans()
        {
            return Ok(_parkingService.VerifyAmountOfVans());
        }
        /// <summary>
        /// 0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES
        /// </summary>
        /// <param name="tipoVaga"></param>
        /// <returns></returns>
        /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES</b>
        /// </remarks>
        [HttpPost("EstacionarVeículos", Name = "Park")]
        public IActionResult Park(ParkingSpotType tipoVeículo, string placaVeiculo)
        {
            bool parkVehicle = _parkingService.Park(tipoVeículo, placaVeiculo);

            if (parkVehicle is true)
            {
                return Ok("Veículo estacionado com sucesso !");
            }
            else
            {
                return Ok("Veículo não pôde ser estacionado, possíveis vagas já se encontram preenchidas !");
            }
        }
        /// <summary>
        /// Digite a placa. EX : AAA-1111
        /// </summary>
        /// <param name="placaVeiculo"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// <b>Digite a placa. EX : AAA-1111</b>
        /// </remarks>
        [HttpPost("RemoverVeículos", Name = "RemovePark")]
        public IActionResult RemovePark(string placaVeiculo)
        {
            bool parkVehicle = _parkingService.RemovePark(placaVeiculo);

            if (parkVehicle is true)
            {
                return Ok("Veículo removido com sucesso !");
            }
            else
            {
                return Ok("Veículo não pôde ser removido, consulte a placa digitada !");
            }
        }
    }
}
