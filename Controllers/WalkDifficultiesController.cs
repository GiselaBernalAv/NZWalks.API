using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultiesController : ControllerBase
    {
        private readonly IWalkDifficultyRepository walkdificultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkdificultyRepository, IMapper mapper)
        {
            this.walkdificultyRepository = walkdificultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetallWalkDiffs()
        {
            var walksDifDomain = await walkdificultyRepository.GetAllWalkDifficulties();

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
            var walksDifDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walksDifDomain);
            return Ok(walksDifDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifById")]
        public async Task<IActionResult> GetWalkDifById(Guid id)
        {
            var walkDifDomain = await walkdificultyRepository.GetWalkDifById(id);

            if(walkDifDomain == null)
            {
                return NotFound();
            }
            var walkDifDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifDomain);

            return Ok(walkDifDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDiff([FromBody] Models.DTO.AddWalkDifficultyRequest addwalkdiffRequest)
        {
            //Request DTO to Domain model
            var walkDifDomain = new WalkDifficulty()
            {
                Code = addwalkdiffRequest.Code
            };

            //Pass details to Repository
            walkDifDomain = await walkdificultyRepository.AddWalkDiff(walkDifDomain);

            //convert back to DTO
            var walkDifDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDifDomain.Id,
                Code = walkDifDomain.Code
             
            };
            return CreatedAtAction(nameof(GetWalkDifById), new { id = walkDifDTO.Id }, walkDifDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDiff([FromRoute] Guid id,
                     [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifRequest)
        {
            //Convert DTO to Domain Model
            var walkDifDomain = new Models.WalkDifficulty()
            {
                Code = updateWalkDifRequest.Code
            };
            //Update Region using repository
            walkDifDomain = await walkdificultyRepository.UpdateWalkDiff(id, walkDifDomain);

            //If null then NotFound
            if(walkDifDomain == null)
            {
                return NotFound();
            }
            //Convert back to domaindto
            var walkDifDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifDomain.Id,
                Code = walkDifDomain.Code
            };
            //Return OK response
            return Ok(walkDifDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDif(Guid id)
        {
            //Get region from database
            var walkDifDomain = await walkdificultyRepository.DeleteWalkDif(id);
            //if null not found
            if(walkDifDomain == null)
            {
                return NotFound();
            }
            //convert response back to dto
            var walkDifDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifDomain);

            //response
            return Ok(walkDifDTO);
        }
    }
}
