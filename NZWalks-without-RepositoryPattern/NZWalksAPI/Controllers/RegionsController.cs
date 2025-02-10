using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain models
            var regions = await dBContext.Regions.ToListAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dBContext.Regions.Find(id);
            // Get Region Domain Model from Database
            var region = await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            // Use DOmain Model to Create Region
            await dBContext.Regions.AddAsync(regionDomainModel);
            await dBContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id},regionDto);
        }

        // Update region
        // PUT: https://localhost:portNo/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists
            var regionDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dBContext.SaveChangesAsync();

            // Convert Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // Delete a region
        // DELETE: https://localhost:portNo/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete region
            dBContext.Regions.Remove(regionDomainModel);
            await dBContext.SaveChangesAsync();

            // return deleted region back
            //map domain model to dto

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

    }
}
