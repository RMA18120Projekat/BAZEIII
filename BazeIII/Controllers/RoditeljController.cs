using Microsoft.AspNetCore.Mvc;
using Skola;

namespace BazeIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoditeljController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveRoditelje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRoditelje()
        {
            
            try
            {
                return new JsonResult(DTOManager.vratiSveRoditelje());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("VratiRoditelja/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetRoditelj([FromRoute(Name = "Id")] int Id)
        {
            try
            {
                return new JsonResult(DTOManager.vratiRoditelja(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
       
        [HttpPut]
        [Route("AzurirajRoditelja/{JmbgUcenika}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AzurirajRoditelja([FromRoute(Name ="JmbgUcenika")]long JmbgUcenika, [FromBody] RoditeljAzur roditelj,int ID)
        {
            try
            {

                UcenikPregled u = DTOManager.vratiUcenika(JmbgUcenika);
                RoditeljPregled ro = DTOManager.vratiRoditelja(ID);
                ro.Ucenik = u;
                ro.Ime = roditelj.Ime;
                ro.Prezime = roditelj.Prezime;
                ro.ClanVeca = roditelj.ClanVeca;
                DTOManager.azurirajRoditelja(ro);
                
                

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        [Route("DodajRoditelja/{Ucenik}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DodajRoditelja([FromRoute(Name ="Ucenik")]long Ucenik,[FromBody] Skola.RoditeljDemo Roditelj)
        {
            try
            {
                UcenikPregled u = DTOManager.vratiUcenika(Ucenik);
                RoditeljPregled p = new RoditeljPregled(Roditelj.ID,Roditelj.Ime,Roditelj.Prezime,Roditelj.ClanVeca,u);
                DTOManager.dodajRoditelja(p);
                                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("ObrisiRoditelja/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ObrisiRoditelja([FromRoute(Name = "Id")] int Id)
        {
            try
            {

                DTOManager.obrisiRoditlja(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
