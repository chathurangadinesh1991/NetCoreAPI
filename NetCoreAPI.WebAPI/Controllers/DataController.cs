using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI.Domain.Models;
using NetCoreAPI.Domain.Services;

namespace NetCoreAPI.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [Route("getAllData")]
        [Authorize(Roles = Policies.Users)]        
        public IActionResult GetAllData()
        {
            var response = _dataService.GetAllData();

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
