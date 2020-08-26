using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController: ControllerBase
    {

        private readonly IDroneQueries  _droneQueries;
        private IMediatrHandler _bus;
        public DroneController(IMediatrHandler bus, IDroneQueries droneQueries)
        {
            _droneQueries = droneQueries;
            _bus = bus;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drone>>> GetDrone()
        {
            return Ok(await _droneQueries.ObterDrones());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Drone>> GetDrone(Guid id)
        {
            var drone = await _droneQueries.ObterDrone(id);

            if (drone == null)
                return NotFound();

            return Ok(drone);
        }

        [HttpGet("{id}/situacao")]
        public async Task<ActionResult<SituacaoViewModel>> GetSituacao(Guid id)
        {
            var drone = await _droneQueries.ObterSituacao(id);

            if (drone == null)
                return NotFound();

            var situacao = await _droneQueries.ObterSituacao(id);

            if (situacao == null)
                return NotFound();

            return Ok(situacao);
        }

        [HttpPost]
        public async Task<ActionResult<Drone>> PostDrone([FromBody]Drone  drone)
        {
            var command = new AdicionarDroneCommand(autonomia: drone.Autonomia, capacidade: drone.Capacidade,velocidade: drone.Velocidade,autonomiaRestante:drone.AutonomiaRestante,carga:drone.Carga);
            var enviado = await _bus.EnviarComando(command);

            if (!enviado)
                return BadRequest();


            return CreatedAtAction("GetDrone", new { id = drone.Id }, drone);
        }
    }
}
