using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{

    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //GET ALL REGIONS
        // GET: https://localhost:portNo/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://www.pathways.co.nz/wp-content/webp-express/webp-images/doc-root/wp-content/uploads/2023/06/bcg-auckland.jpg.webp"

                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/9c/Wellington_Region_location_in_New_Zealand.svg"

                },
            };
            return Ok(regions);
        }
    }
}
