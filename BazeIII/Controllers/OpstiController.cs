using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpstiController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveOpste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOpste()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveOpste());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiOpsti/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOpsti([FromRoute(Name = "Naziv")] string Naziv)
        {
            try
            {
                return new JsonResult(DTOManager.vratiOpsti(Naziv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajOpsti")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajOpsti([FromBody] Skola.OpstiDemo Opsti)
        {
            try
            {
                OpstiPregled p = new OpstiPregled();
                p.ImePredmeta = Opsti.ImePredmeta;
                p.Godina_studija = Opsti.Godina_studija;
                p.UkupanBrojSmerova = Opsti.UkupanBrojSmerova;

                DTOManager.dodajOpsti(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajOpsti/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajOpsti([FromRoute(Name = "Naziv")] string Naziv, [FromBody] Skola.OpstiAzur Opsti)
        {
            try
            {
                OpstiPregled p = DTOManager.vratiOpsti(Naziv);
                p.Godina_studija = Opsti.Godina_studija;
                p.UkupanBrojSmerova = Opsti.UkupanBrojSmerova;
                DTOManager.azurirajOpsti(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodeliSmerOpstem/{Smer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SmerOpstem([FromRoute(Name = "Smer")] string Smer, [FromBody] string Opsti)
        {
            try
            {
                DTOManager.dodeliSmerOpstem(Smer, Opsti);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiOpsti/{Naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiPredmet([FromRoute(Name = "Naziv")] string Naziv)
        {
            try
            {

                DTOManager.obrisiOpsti(Naziv);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
