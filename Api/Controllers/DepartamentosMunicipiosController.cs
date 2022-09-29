using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentosMunicipiosController : Controller
    {
        private readonly IDepartamentosMunicipios _depMun;

        public DepartamentosMunicipiosController(IDepartamentosMunicipios DM)
        {
            _depMun = DM;
        }

        [HttpGet("GetDepartamentos")]
        public async Task<IActionResult> GetDepartamentos()
        {
            return await _depMun.GetDepartamentos();
        }

        [HttpGet("GetMunicipios/{codDepartento}")]
        public async Task<IActionResult> GetMunicipios(int codDepartento)
        {
            return await _depMun.GetMunicipios(codDepartento);
        }
    }
}
