using Application.Interfaces;
using Domain.Entities;
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
        /// <summary>
        /// Retorna a quantidade de vagas vazias
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterNumeroVagasVazias", Name = "GetEmptySpots")]
        public IActionResult GetEmptySpots() =>
            Ok(_parkingService.GetEmptySpots());

        /// <summary>
        /// Retorna a quantidade de vagas total no estacionamento
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterNumeroDeVagasTotal", Name = "GetTotalVacancy")]
        public IActionResult GetTotalSpots() =>
            Ok(_parkingService.GetTotalSpots());

        /// <summary>
        /// Verifica se o estacionamento está totalmente cheio
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarEstacionamentoCheio", Name = "VerifyFullParkingLot")]
        public IActionResult VerifyFullParkingLot() =>
            Ok(_parkingService.VerifyFullParkingLot());

        /// <summary>
        /// Verifica se o estacionamentoe está totalmente vazio
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarEstacionamentoVazio", Name = "VerifyEmptyParkingLot")]
        public IActionResult VerifyEmptyParkingLot() =>
            Ok(_parkingService.VerifyEmptyParkingLot());


        /// <summary>
        /// Verifica se todas as vagas de um determinado tipo estão preenchidas
        /// </summary>
        /// <param name="tipoVaga"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES</b>
        /// </remarks>
        [HttpGet("VerificarVagasCheiasPorTipo", Name = "VerifyFullGroupType")]
        public IActionResult VerifyFullGroupType(ParkingSpotType tipoVaga) =>
             Ok(_parkingService.VerifyFullGroupType(tipoVaga));

        /// <summary>
        /// Verificar quantidade de vagas as vans estão ocupando
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarQuantidaVagasVansOcupam", Name = "VerifyAmountOfVans")]
        public IActionResult VerifyAmountOfVans() =>
            Ok(_parkingService.VerifyAmountOfVans());

        /// <summary>
        /// Verificar Placas Estacionadas
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarPlacasEstacionadas", Name = "GetLicensesPlates")]
        public IActionResult GetLicensesPlates()
        {
            List<LicensesPlates> licensesPlates = _parkingService.ReturnLicensesPlates();
            if (licensesPlates.Any())
                return Ok(_parkingService.ReturnLicensesPlates());
            else
                return NoContent();

        }

        /// <summary>
        /// Realiza a inserção de um veículo em uma vaga
        /// </summary>
        /// <param name="tipoVeículo"></param>
        /// <param name="placaVeiculo"></param>
        /// <returns></returns>
        /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VEÍCULOS GRANDES</b>
        /// </remarks>
        [HttpPost("EstacionarVeículos", Name = "Park")]
        public IActionResult Park(ParkingSpotType tipoVeículo, string placaVeiculo)
        {
            ParkResult resultPark = new();

            if (_parkingService.ValidLicensePlate(placaVeiculo))
                resultPark = _parkingService.Park(tipoVeículo, placaVeiculo);
            else
                return BadRequest("Placa digitada fora do padrão comum ou mercosul !");

            if (resultPark.Parked)
                return Ok(resultPark);
            else
                return BadRequest(resultPark);
        }

        /// <summary>
        /// Remove um determinado carro de uma vaga pela placa
        /// </summary>
        /// <param name="placaVeiculo"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// <b>Digite a placa. EX : AAA-1111</b>
        /// </remarks>
        [HttpPost("RemoverVeículos", Name = "RemovePark")]
        public IActionResult RemovePark(string placaVeiculo) =>
            _parkingService.ValidLicensePlate(placaVeiculo) is true ? Ok(_parkingService.RemovePark(placaVeiculo)) : BadRequest("Placa digitada fora do padrão comum ou mercosul !");

        /// <summary>
        /// Limpar estacionamento, removendo todos os veículos.
        /// </summary>
        /// <returns></returns>
        [HttpPost("LimparEstacionamento", Name = "ClearPark")]
        public IActionResult ClearPark() =>
             Ok(_parkingService.ClearPark());

    }
}
