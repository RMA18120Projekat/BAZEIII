using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrojTelefonaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveBrojeve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBrojeve()
        {
            try
            {
                return new JsonResult(DTOManager.vratiSveBrojeve());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiBrojTelefona/{broj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBroj([FromRoute(Name = "broj")] int broj)
        {
            try
            {
                return new JsonResult(DTOManager.vratiBroj(broj));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("DodajBrojTelefona/{IdRoditelja}/{broj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajBroj([FromRoute(Name = "IdRoditelja")] int IdRoditelja, [FromRoute(Name = "broj")] int broj)
        {
            try
            {


                DTOManager.dodajBroj(broj,IdRoditelja);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajBrojTelefona/{vrednost}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajBrojTelefona([FromRoute(Name = "vrednost")] int vrednost, [FromBody] int roditelj)
        {
            try
            {

                DTOManager.azurirajBroj(vrednost,roditelj);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiBrojTelefona/{vrednost}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiBrojTelefona([FromRoute(Name = "vrednost")] int vrednost)
        {
            try
            {

                DTOManager.obrisiBrojTelefona(vrednost);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
