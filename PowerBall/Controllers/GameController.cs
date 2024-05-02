using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerBall.Repository.Interefaces;

namespace PowerBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameRepo _gamerepo;

        public GameController(IGameRepo context)
        {
            _gamerepo = context;
        }


        [HttpPost("Play-Game")]
        public async Task<IActionResult> PlayGame(int GameCount)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return new BadRequestObjectResult("Invalid or missing user information in the token.");
            }
            return await _gamerepo.PlayGame(userId, GameCount);
        }

    }
}

