using Microsoft.AspNetCore.Mvc;
using Theatre.Application.Contracts;
using Theatre.Domain.Entities;

namespace Theatre.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepo;
        private readonly ILogger _logger;
        public ActorController(ILogger<ActorController> logger, IActorRepository actorRepo)
        {
            _actorRepo = actorRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogCritical("At Actor Control");
            IEnumerable<Actors> list = await  _actorRepo.GetAll();
            return Ok(list);
        }
    }
}
