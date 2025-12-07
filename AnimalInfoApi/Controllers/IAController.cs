using Microsoft.AspNetCore.Mvc;
using AnimalInfoApi.Services;

namespace AnimalInfoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IAController : ControllerBase
    {
        private readonly IIAService _iaService;

        public IAController(IIAService iaService)
        {
            _iaService = iaService;
        }

        [HttpPost("descripcion")]
        public async Task<IActionResult> GenerarDescripcion([FromBody] Dictionary<string, string> body)
        {
            if (!body.ContainsKey("animal"))
                return BadRequest("Falta el parámetro 'animal'");

            var texto = await _iaService.GenerarDescripcion(body["animal"]);

            return Ok(new { descripcion = texto });
        }
    }
}