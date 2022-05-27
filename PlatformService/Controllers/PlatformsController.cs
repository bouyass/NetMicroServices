using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _plaformRepo;
        private readonly IMapper _mapper;
        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _plaformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms....");

            var platformItem = _plaformRepo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _plaformRepo.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
        {
            if(platform != null)
            {
                var platformModel = _mapper.Map<Platform>(platform);
                _plaformRepo.CreatePlatform(platformModel);
                _plaformRepo.SaveChanges();

                var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

                return platformReadDto;
            }
            else
            {
                throw new Exception("Invalid arguements");
            }

        }
    }
}
