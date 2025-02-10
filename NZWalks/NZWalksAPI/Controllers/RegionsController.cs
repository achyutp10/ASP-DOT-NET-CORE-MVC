using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

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
            // Get Data From Database - Domain models
            var regions = dBContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //Return Dtos
            return Ok(regionDto);
        }

        // GET SINGLE REGION (By ID)
        // GET: https://localhost:portNo/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dBContext.Regions.Find(id);
            // Get Region Domain Model from Database
            var region = dBContext.Regions.FirstOrDefault(x=>x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            // Map region domain model to region dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };
            // Return DTO back to client
            return Ok(regionDto);
        }

        // POST To Create New Region
        // POST: https://localhost:portNo/api/regions

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            // Use DOmain Model to Create Region
            dBContext.Regions.Add(regionDomainModel);
            dBContext.SaveChanges();

            // Map DOmain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id},regionDto);
        }
    }
}
