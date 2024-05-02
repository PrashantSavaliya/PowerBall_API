using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace PowerBall.Repository.Interefaces
{
    public interface IResultRepo
    {
        Task<IActionResult> topwinners(int drawid);
        IActionResult WinnersAll(int drawid);
        IActionResult ResultByUserId(int userid);
    }
}
