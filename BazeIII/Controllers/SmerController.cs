using Microsoft.AspNetCore.Mvc;
using Skola;
namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmerController : ControllerBase
    {
      
            [HttpGet]
            [Route("VratiSveSmerove")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult GetSmerove()
            {
                try
                {
                    return new JsonResult(DTOManager.vratiSveSmerove());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            [HttpGet]
            [Route("VratiSmer/{Naziv}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult GetSmer([FromRoute(Name = "Naziv")] string Naziv)
            {
                try
                {
                    return new JsonResult(DTOManager.vratiSmer(Naziv));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            [HttpPost]
            [Route("DodajSmer")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult DodajSmer([FromBody] Skola.SmerDemo Smer)
            {
                try
                {

                    DTOManager.dodajSmer(Smer);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpPut]
            [Route("AzurirajSmer/{Naziv}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult AzurirajSmer([FromRoute(Name = "Naziv")] string Naziv, [FromBody] Skola.SmerAzur Smer)
            {
                try
                {

                    DTOManager.azurirajSmer(Smer,Naziv);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
        [HttpPost]
        [Route("DodeliOpstiSmeru/{NazivPredmeta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult OpstiSmeru([FromRoute(Name = "NazivPredmeta")] string NazivPredmeta, [FromBody] string Smer)
        {
            try
            {
                DTOManager.dodeliOpstiSmeru(NazivPredmeta, Smer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
            [Route("ObrisiSmer/{Naziv}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult ObrisiSmer([FromRoute(Name = "Naziv")] string Naziv)
            {
                try
                {

                    DTOManager.obrisiSmer(Naziv);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }


        }

    
}
