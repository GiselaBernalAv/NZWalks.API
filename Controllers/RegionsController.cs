using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;
using NZWalks.API.Models;
using AutoMapper;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetallRegions()
        {
            var regions = await regionRepository.GetAllRegions();

            //return regionDTO
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code=region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        LongReg = region.LongReg,
            //        Population = region.Population
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionById")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await regionRepository.GetRegionById(id);

            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegions(Models.DTO.AddRegionRequest addregionreq)
        {
            //Request DTO to Domain model
            var region = new Region()
            {
                Code = addregionreq.Code,
                Area = addregionreq.Area,
                Lat = addregionreq.Lat,
                LongReg = addregionreq.LongReg,
                Name = addregionreq.Name,
                Population = addregionreq.Population
            };

            //Pass details to Repository
            region = await regionRepository.AddRegion(region);

            //convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                LongReg = region.LongReg,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteRegion(id);
            //if null not found
            if(region == null)
            {
                return NotFound();
            }
            //convert response back to dto
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
               
            //response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain Model
            var region = new Models.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                LongReg = updateRegionRequest.LongReg,
                Population = updateRegionRequest.Population
            };
            //Update Region using repository
            region = await regionRepository.UpdateRegion(id, region);

            //If null then NotFound
            if(region == null)
            {
                return NotFound();
            }
            //Convert back to domaindto
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                LongReg = region.LongReg,
                Population = region.Population
            };
            //Return OK response
            return Ok(regionDTO);
        }
    }
}
