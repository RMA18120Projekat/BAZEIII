using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class NeNasatavnoController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSvaNeNastavnaOsoblja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSvaNeNastavna()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveNeNastavno());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiNeNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNeNastavno([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {
                return new JsonResult(DTOManager.vratiNeNastavno(Jmbg));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajNeNastavno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajNeNastavno([FromBody] Skola.NeNastavnoDemo NeNastavno)
        {
            try
            {
                NeNastavnoPregled p = new NeNastavnoPregled(NeNastavno.Jmbg,NeNastavno.Ime,NeNastavno.Prezime,NeNastavno.Adresa,NeNastavno.DatumRodjenja,NeNastavno.SektorRada,NeNastavno.StrucnaSprema);

                DTOManager.dodajNeNastavno(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajNeNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajNeNastavno([FromRoute(Name = "Jmbg")] long Jmbg, [FromBody] Skola.NeNastavnoAzur NeNastavno)
        {
            try
            {
                NeNastavnoPregled objekatIzBaze = DTOManager.vratiNeNastavno(Jmbg);
                objekatIzBaze.Ime = NeNastavno.Ime;
                objekatIzBaze.Prezime = NeNastavno.Prezime;
                objekatIzBaze.Adresa = NeNastavno.Adresa;
                objekatIzBaze.DatumRodjenja = NeNastavno.DatumRodjenja;
                objekatIzBaze.StrucnaSprema = NeNastavno.StrucnaSprema;
                objekatIzBaze.SektorRada = NeNastavno.SektorRada;
                

                DTOManager.azurirajNeNastavno(objekatIzBaze);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiNeNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiNeNastavno([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {

                DTOManager.obrisiNeNastavno(Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
