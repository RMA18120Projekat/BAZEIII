using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ZaposleniController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveZaposlene")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetZaposleni()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveZaposlene());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiZaposlene/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetZaposleni([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {
                return new JsonResult(DTOManager.vratiZaposlenig(Jmbg));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajZaposlenog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajZaposlenog([FromBody] Skola.ZaposleniDemo Zaposleni)
        {
            try
            {
                ZaposleniPregled p = new ZaposleniPregled(Zaposleni.Jmbg,Zaposleni.Ime,Zaposleni.Prezime,Zaposleni.Adresa,Zaposleni.DatumRodjenja);

                DTOManager.dodajZaposlenig(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajZaposlenog/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajZaposleni([FromRoute(Name = "Jmbg")] long Jmbg, [FromBody] Skola.ZaposleniAzur Zaposleni)
        {
            try
            {
                ZaposleniPregled p = DTOManager.vratiZaposlenig(Jmbg);
                p.Ime = Zaposleni.Ime;
                p.Prezime = Zaposleni.Prezime;
                p.Adresa = Zaposleni.Adresa;
                p.DatumRodjenja = Zaposleni.DatumRodjenja;
                
                DTOManager.azurirajZaposlenog(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiZaposleni/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiZaposlenog([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {

                DTOManager.obrisiZaposlenog(Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
