using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NastavnoController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSvaNastavnaOsoblja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSvaNastavna()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveNastavno());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNastavno([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {
                return new JsonResult(DTOManager.vratiNastavno(Jmbg));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajNastavno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajNastavno([FromBody] Skola.NastavnoDemo Nastavno)
        {
            try
            {
                NastavnoPregled p = new NastavnoPregled(Nastavno.Jmbg,Nastavno.Ime,Nastavno.Prezime,Nastavno.Adresa,Nastavno.DatumRodjenja,Nastavno.Norma,Nastavno.Angazovan,Nastavno.BrojCasova,Nastavno.NazivSkole );

                DTOManager.dodajNastavno(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajNastavno([FromRoute(Name = "Jmbg")] long Jmbg, [FromBody] Skola.NastavnoAzuriraj Nastavno)
        {
            try
            {
                NastavnoPregled objekatIzBaze = DTOManager.vratiNastavno(Jmbg);
                objekatIzBaze.Ime = Nastavno.Ime;
                objekatIzBaze.Prezime = Nastavno.Prezime;
                objekatIzBaze.Adresa = Nastavno.Adresa;
                objekatIzBaze.DatumRodjenja = Nastavno.DatumRodjenja;

                DTOManager.azurirajNastavno(objekatIzBaze);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiNastavno/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiNastavno([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {

                DTOManager.obrisiNastavno(Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
