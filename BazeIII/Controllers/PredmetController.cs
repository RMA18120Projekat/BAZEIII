using Microsoft.AspNetCore.Mvc;
using Skola;
namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredmetController:ControllerBase
    {
        [HttpGet]
        [Route("VratiSvePredmete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPredmete()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSvePredmete());
            }
            catch(Exception ex) { 
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiPredmet/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPredmet([FromRoute(Name ="Naziv")]string Naziv)
        {
            try
            {
                return new JsonResult(DTOManager.vratiPredmet(Naziv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajPredmet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajPredmet([FromBody] Skola.PredmetDemo Predmet)
        {
            try
            {
                PredmetPregled p = new PredmetPregled(Predmet.ImePredmeta,Predmet.Godina_studija);
                
               DTOManager.dodajPredmet(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodeliUcenikaPredmetu/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UcenikPredmetu([FromRoute(Name = "Jmbg")] long Jmbg, [FromBody] string Naziv)
        {
            try
            {
                DTOManager.dodeliUcenikaPredmetu(Jmbg, Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
    [Route("AzurirajPredmet/{Naziv}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AzurirajPredmet([FromRoute(Name ="Naziv")]string Naziv,[FromBody] Skola.PredmetAzur Predmet)
    {
        try
        {
                PredmetPregled p = DTOManager.vratiPredmet(Naziv);
                p.Godina_studija = Predmet.Godina_studija;
            DTOManager.azurirajPredmet(p);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
        [HttpDelete]
        [Route("ObrisiPredmet/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiPredmet([FromRoute(Name ="Naziv")] string Naziv)
        {
            try
            {

                DTOManager.obrisiPredmet(Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}