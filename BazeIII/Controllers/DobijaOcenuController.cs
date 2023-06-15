using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DobijaOcenuController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveDobijeneOcene")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetDobijene()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveDobijeneOcene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiDobijenuOcenu/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAngazovan([FromRoute(Name = "Id")] int Id)
        {
            try
            {
                return new JsonResult(DTOManager.vratiDobija(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajDobijenuOcenu/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajDobijaOcenu([FromRoute(Name = "Jmbg")] long Jmbg, [FromBody] Skola.DobijaOcenuDemo dobija, string Naziv,int IdOcene)
        {
            try
            {
                
                DTOManager.dodajDobija(dobija,Jmbg,Naziv,IdOcene);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajDovijenuOcenu/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajDobijenuOcenu([FromRoute(Name = "Id")] int Id, [FromBody] Skola.DobijaOcenuAzur dobija, long Jmbg, string Predmet,int IdOcene)
        {
            try
            {
                
                DTOManager.azurirajDobija(dobija,Jmbg,Predmet,IdOcene,Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiDobijenuOcenu/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiDobijenuOcenu([FromRoute(Name = "Id")] int Id)
        {
            try
            {

                DTOManager.obrisiDobija(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
