using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;
using NZWalks.API.Models;
using AutoMapper;

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
    }
}
