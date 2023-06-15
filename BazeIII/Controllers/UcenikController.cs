using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UcenikController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveUcenike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUcenike()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveUcenike());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiUcenika/{JMBG}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUcenik([FromRoute(Name = "JMBG")] long JMBG)
        {
            try
            {
                return new JsonResult(DTOManager.vratiUcenika(JMBG));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodeliPredmetUceniku/{NazivPredmeta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PredmetUceniku([FromRoute(Name = "NazivPredmeta")] string NazivPredmeta, [FromBody]long Jmbg)
        {
            try
            {
                DTOManager.dodeliPredmetUceniku(NazivPredmeta, Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("OduzmiPredmetUceniku/{NazivPredmeta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult OduzmiPredmetUceniku([FromRoute(Name = "NazivPredmeta")] string NazivPredmeta, [FromBody] long Jmbg)
        {
            try
            {
                DTOManager.dodeliPredmetUceniku(NazivPredmeta, Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("DodajUcenika/{Smer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajUcenika([FromRoute(Name ="Smer")]string Smer, [FromBody] Skola.UcenikDemo ucenik)
        {
            try
            {
                SmerPregled zmer = DTOManager.vratiSmer(Smer);
                UcenikPregled u=new UcenikPregled(ucenik.Jmbg, ucenik.Ime, ucenik.Prezime,ucenik.Adresa, ucenik.Razred,ucenik.JUB,zmer, ucenik.DatumUpisa);
                DTOManager.dodajUcenika(u);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajUcenika/{Smer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajUenika([FromRoute(Name ="Smer")]string Smer,[FromBody] UcenikAzur ucenik,long Jmbg)
        {
            try
            {
                
                DTOManager.azurirajUcenika(ucenik,Smer,Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiUcenika/{Jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiUcenika([FromRoute(Name = "Jmbg")] long Jmbg)
        {
            try
            {

                DTOManager.obrisiUcenika(Jmbg);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
