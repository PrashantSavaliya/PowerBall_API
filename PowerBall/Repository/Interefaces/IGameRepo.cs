using Microsoft.AspNetCore.Mvc;
using PowerBall.Models;

namespace PowerBall.Repository.Interefaces
{
    public interface IGameRepo
    {
        Task<IActionResult> PlayGame(int userid, int GameCount);
    }
}
