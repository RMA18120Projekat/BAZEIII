using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcenaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveOcene")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOcene()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveOcene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiOcenu/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOcenu([FromRoute(Name = "Id")] int Id)
        {
            try
            {
                return new JsonResult(DTOManager.vratiOcenu(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajOcenu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajOcenu([FromBody] Skola.OcenaDemo Ocena)
        {
            try
            {
                OcenaPregled o = new OcenaPregled(Ocena.ID, Ocena.Vrednost, Ocena.TekstualniOpis);
                DTOManager.dodajOcenu(o);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajOcenu/{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajOcenu([FromRoute(Name ="ID")]int ID,[FromBody] Skola.OcenaAzur Ocena)
        {
            try
            {
                OcenaPregled c = DTOManager.vratiOcenu(ID);
                c.Vrednost=Ocena.Vrednost;
                c.TekstualniOpis = Ocena.TekstualniOpis;
                DTOManager.azurirajOcenu(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiOcenu/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiOcenu([FromRoute(Name = "Id")] int Id)
        {
            try
            {

                DTOManager.obrisiOcenu(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
