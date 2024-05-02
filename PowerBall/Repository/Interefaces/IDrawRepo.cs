using Microsoft.AspNetCore.Mvc;

namespace PowerBall.Repository.Interefaces
{
    public interface IDrawRepo
    {
        Task<IActionResult> Draw(int drawid);
    }
}
