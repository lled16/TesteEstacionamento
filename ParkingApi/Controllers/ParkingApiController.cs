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
        /// Verifica se o estacionamento est� totalmente cheio
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarEstacionamentoCheio", Name = "VerifyFullParkingLot")]
        public IActionResult VerifyFullParkingLot() =>
            Ok(_parkingService.VerifyFullParkingLot());

        /// <summary>
        /// Verifica se o estacionamentoe est� totalmente vazio
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificarEstacionamentoVazio", Name = "VerifyEmptyParkingLot")]
        public IActionResult VerifyEmptyParkingLot() =>
            Ok(_parkingService.VerifyEmptyParkingLot());


        /// <summary>
        /// Verifica se todas as vagas de um determinado tipo est�o preenchidas
        /// </summary>
        /// <param name="tipoVaga"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VE�CULOS GRANDES</b>
        /// </remarks>
        [HttpGet("VerificarVagasCheiasPorTipo", Name = "VerifyFullGroupType")]
        public IActionResult VerifyFullGroupType(ParkingSpotType tipoVaga) =>
             Ok(_parkingService.VerifyFullGroupType(tipoVaga));

        /// <summary>
        /// Verificar quantidade de vagas as vans est�o ocupando
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
        /// Realiza a inser��o de um ve�culo em uma vaga
        /// </summary>
        /// <param name="tipoVe�culo"></param>
        /// <param name="placaVeiculo"></param>
        /// <returns></returns>
        /// <remarks>
        /// <b>0 = MOTO | 1 = CARRO | 2 = VANS OU VE�CULOS GRANDES</b>
        /// </remarks>
        [HttpPost("EstacionarVe�culos", Name = "Park")]
        public IActionResult Park(ParkingSpotType tipoVe�culo, string placaVeiculo)
        {
            ParkResult resultPark = new();

            if (_parkingService.ValidLicensePlate(placaVeiculo))
                resultPark = _parkingService.Park(tipoVe�culo, placaVeiculo);
            else
                return BadRequest("Placa digitada fora do padr�o comum ou mercosul !");

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
        [HttpPost("RemoverVe�culos", Name = "RemovePark")]
        public IActionResult RemovePark(string placaVeiculo) =>
            _parkingService.ValidLicensePlate(placaVeiculo) is true ? Ok(_parkingService.RemovePark(placaVeiculo)) : BadRequest("Placa digitada fora do padr�o comum ou mercosul !");

        /// <summary>
        /// Limpar estacionamento, removendo todos os ve�culos.
        /// </summary>
        /// <returns></returns>
        [HttpPost("LimparEstacionamento", Name = "ClearPark")]
        public IActionResult ClearPark() =>
             Ok(_parkingService.ClearPark());

    }
}
