using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetallWalks()
        {
            var walksDomain = await walkRepository.GetAllWalks();

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
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomain = await walkRepository.GetWalkById(id);

            if(walkDomain == null)
            {
                return NotFound();
            }
            var walknDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walknDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalks([FromBody] Models.DTO.AddWalkRequest addwalkRequest)
        {
            //Request DTO to Domain model
            var walkDomain = new Walk()
            {
                LongWalk = addwalkRequest.LongWalk,
                Name = addwalkRequest.Name,
                RegionId = addwalkRequest.RegionId,
                WalkDifficultyId = addwalkRequest.WalkDifficultyId,
            };

            //Pass details to Repository
            walkDomain = await walkRepository.AddWalk(walkDomain);

            //convert back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                LongWalk = walkDomain.LongWalk,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, 
                     [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO to Domain Model
            var walkDomain = new Models.Walk()
            {
                LongWalk = updateWalkRequest.LongWalk,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            //Update Region using repository
            walkDomain = await walkRepository.UpdateWalk(id, walkDomain);

            //If null then NotFound
            if(walkDomain == null)
            {
                return NotFound();
            }
            //Convert back to domaindto
            var walknDTO = new Models.DTO.Walk()
            {
                Id = walkDomain.Id,
                LongWalk = walkDomain.LongWalk,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };
            //Return OK response
            return Ok(walknDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            //Get region from database
            var walkDomain = await walkRepository.DeleteWalk(id);
            //if null not found
            if(walkDomain == null)
            {
                return NotFound();
            }
            //convert response back to dto
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //response
            return Ok(walkDTO);
        }
    }
}
