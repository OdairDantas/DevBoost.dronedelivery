using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Application.Events;
using MediatR;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;

namespace DevBoost.DroneDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IDroneService _droneService;
        private readonly IPedidoService _pedidoService;
        private IMediatrHandler _bus;
        public PedidoController(IMediatrHandler bus, IPedidoService pedidoService, IDroneService droneService)
        {
            _droneService = droneService;
            _pedidoService = pedidoService;
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
            await _droneService.AtualizarStatusDronesAsync();
            return Ok(await Task.FromResult(_pedidoService.ObterTodos()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _pedidoService.ObterByIdAsync(id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.Id)
                return BadRequest();
            try
            {
                await _pedidoService.UpdateAsync(pedido);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_pedidoService.PedidoExiste(id))
                    return NotFound();
                else
                    throw;

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido([FromBody]Pedido pedido)
        {
            if (pedido.Peso > 12000)
                return BadRequest("Rejeitado: Pedido acima do peso máximo aceito.");

            var distancia = new Localizacao() { Latitude = (double)pedido.Latitude, Longitude = (double)pedido.Longitude }.CalcularDistanciaEmKilometros() * 2;
            var drones = _droneService.ObterTodos().Result.ToList();
            if (!drones.Any())
                return BadRequest("Rejeitado: Sem entregadores disponiveis.");

            var tempoTrajetoCompleto = (distancia / (drones.Sum(d => d.Velocidade) / drones.Count)) * 60;

            if (tempoTrajetoCompleto > (drones.Sum(d => d.Autonomia) / drones.Count))
                return BadRequest("Rejeitado: Fora da área de entrega.");

            var noovoPedido = new Pedido() { DataHora = DateTime.Now, Peso = pedido.Peso, Status = EStatusPedido.AguardandoEntregador, Latitude = pedido.Latitude, Longitude = pedido.Longitude };
            await _pedidoService.Criar(noovoPedido);


            return CreatedAtAction("GetPedido", new { id = noovoPedido.Id }, noovoPedido);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(Guid id)
        {

            var pedido = await _pedidoService.DeleteAsync(id);
            if (pedido == null)
                return NotFound();

            return pedido;
        }

    }
}
