using ApiMasivTest.Models;
using ApiMasivTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMasivTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        /// <summary>
        /// Return a list with the roulettes ans their current state, default endpoint.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoulette()
        {
            try 
            {
                RouletteService rouletList = new RouletteService();

                return Ok(rouletList.listRoulette());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return StatusCode(405);
            }
        }
        /// <summary>
        /// Create a new rulette
        /// </summary>
        /// <param name="roulette"></param>
        /// <returns></returns>
        [HttpPost("NewRoulette")]
        public IActionResult PostRoulette(RouletteModel roulette)
        {
            try 
            {
                RouletteService newRoulette = new RouletteService();
                newRoulette.createRoulette(roulette);

                return CreatedAtAction(nameof(PostRoulette), "Se ha registrado la ruleta con ID: " + roulette.Id);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e);
                return StatusCode(405);
            }
        }
        /// <summary>
        /// Open a ruletta, requires ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("OpenRulette/{id}")]
        public IActionResult PutRoullete([FromRoute(Name = "id")] int id)
        {
            try
            {
                RouletteService updateRoulette = new RouletteService();
                var putStateRoulette = updateRoulette.updateStateRouletteOpen(id);
                if (putStateRoulette != null)
                {
                    var actualStatus = putStateRoulette.IsOpenRulette == true ? "abierta" : "cerrada";

                    return CreatedAtAction(nameof(PutRoullete), "La ruleta " + putStateRoulette.Id + " se encuentra " + actualStatus);
                }
                else
                {
                    return CreatedAtAction(nameof(PutRoullete), "Error: La ruleta " + id + " no esta creada o ya se encuentra abierta.");
                } 
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e);
                return StatusCode(405);
            }
        }
        /// <summary>
        /// Register a bet on a roulette
        /// </summary>
        /// <returns></returns>
        [HttpPost("RouletteBet/{id}")]
        public IActionResult BetRoullette([FromHeader(Name = "Id-User")] string userId, [FromRoute(Name = "Id")] int id, [FromBody] BetRoulette dataBet)
        {
            try
            {
                RouletteService dataBetRoulette = new RouletteService();
                var betRoulette = dataBetRoulette.betRoulette(userId, id, dataBet.number, dataBet.money);
                if (betRoulette != null)
                {
                    return CreatedAtAction(nameof(BetRoullette), "Se ha registrado la apuesta en la ruleta " + id);
                }
                else
                {
                    return CreatedAtAction(nameof(BetRoullette), "No se ha registrado la apuesta, validar los datos enviados a la ruleta: " + id);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return StatusCode(405);
            }
        }
        /// <summary>
        /// Roulette closing and results
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("CloseRulette/{id}")]
        public IActionResult PutRoulleteClose([FromRoute(Name = "id")] int id)
        {
            try
            {
                RouletteService updateCloseRoulette = new RouletteService();
                var putCloseRoulette = updateCloseRoulette.updateStateRouletteClose(id);
                if (putCloseRoulette != null)
                {
                    string resultsRouletts = updateCloseRoulette.resultsRoulette(id);

                    return CreatedAtAction(nameof(PutRoulleteClose), resultsRouletts);
                }
                else
                {
                    return CreatedAtAction(nameof(PutRoulleteClose), "Error: La ruleta " + id + " no esta creada o ya se encuentra Cerrada.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return StatusCode(405);
            }
        }
    }
}
