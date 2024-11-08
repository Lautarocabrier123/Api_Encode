using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using TrabajoApi.Context;
using TrabajoApi.DTOs;
using TrabajoApi.services;

namespace TrabajoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;
        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get()
        {
            return Ok(await _perfilService.lista());
        }
    }
}