using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngazovanController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveAngazovane")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAngazovani()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveAngazovane());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiAngazovane/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAngazovan([FromRoute(Name = "Id")] int Id)
        {
            try
            {
                return new JsonResult(DTOManager.vratiAngazovan(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajAngazovanog/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajAngazovanog([FromRoute(Name ="Jmbg")]long Jmbg,[FromBody] Skola.AngazovanDemo Angazovani,string Naziv)
        {
            try
            {
                
                
                DTOManager.dodajAngazovan(Angazovani,Jmbg,Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajAngazovanog/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajAngazovanog([FromRoute(Name = "Id")] int Id, [FromBody] Skola.AngazovanAzur Angazovani,long Jmbg,string Predmet)
        {
            try
            {
                
                DTOManager.azurirajAngazovan(Angazovani,Jmbg,Predmet,Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiAngazovanog/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiAngazovanog([FromRoute(Name = "Id")] int Id)
        {
            try
            {

                DTOManager.obrisiAngazovan(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
