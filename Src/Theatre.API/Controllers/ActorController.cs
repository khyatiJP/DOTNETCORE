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
        public ActorController(IActorRepository actorRepo)
        {
            _actorRepo = actorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Actors> list = await  _actorRepo.GetAll();
            return Ok(list);
        }
    }
}
