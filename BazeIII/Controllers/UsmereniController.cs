using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsmereniController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveUsmerene")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsmereni()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveUsmrene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiUsmereni/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsmeren([FromRoute(Name = "Naziv")] string Naziv)
        {
            try
            {
                return new JsonResult(DTOManager.vratiUsmereni(Naziv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajUsmereni/{Smer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajUsmereni([FromRoute(Name ="Smer")]string Smer,[FromBody] Skola.UsmereniDemo Usmereni)
        {
            try
            {
                Skola.SmerPregled smer = DTOManager.vratiSmer(Smer);
                Skola.UsmereniPregled usmereni = new Skola.UsmereniPregled();
                
                usmereni.ImePredmeta = Usmereni.ImePredmeta;
                usmereni.Godina_studija = Usmereni.Godina_studija;
                usmereni.Smer = smer;
                DTOManager.dodajUsmereni(usmereni);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajUsmereni/{Smer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajUsmereni([FromRoute(Name ="Smer")]string Smer,[FromBody] Skola.UsmereniAzur Usmereni,string Naziv)
        {
            try
            {

               
                DTOManager.azurirajUsmereni(Usmereni,Smer,Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiUsmereni/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiUsmereni([FromRoute(Name = "Naziv")] string Naziv)
        {
            try
            {

                DTOManager.obrisiUsmereni(Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
