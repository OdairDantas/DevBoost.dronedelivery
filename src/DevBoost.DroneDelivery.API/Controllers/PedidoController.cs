using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using DevBoost.DroneDelivery.Application.Events;

namespace DevBoost.DroneDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries  _pedidoQueries;
        private IMediatrHandler _bus;
        public PedidoController(IMediatrHandler bus, IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoViewModel>>> GetPedido()
        {
            return Ok(await _pedidoQueries.ObterPedidos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoViewModel>> GetPedido(Guid id)
        {
            var pedido = await _pedidoQueries.ObterPedido(id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] PedidoViewModel pedido)
        {
            var command = new AdicionarPedidoCommand(peso: pedido.Peso, latitude:pedido.Latitude,longitude : pedido.Longitude);
            var enviado = await _bus.EnviarComando(command);

            if (!enviado)
                return BadRequest();

            await _bus.PublicarEvento(new SolicitarEntregaEvent(command.Id));

            return CreatedAtAction("GetPedido", new {Sucess=true, command.Id });
            
        }
    }
}
