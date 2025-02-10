using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{

    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;

        public RegionsController(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;    
        }
        //GET ALL REGIONS
        // GET: https://localhost:portNo/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dBContext.Regions.ToList();
            return Ok(regions);
        }

        // GET SINGLE REGION (By ID)
        // GET: https://localhost:portNo/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            //var region = dBContext.Regions.Find(id);
            var region = dBContext.Regions.FirstOrDefault(x=>x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(region);
            }
        }
    }
}
