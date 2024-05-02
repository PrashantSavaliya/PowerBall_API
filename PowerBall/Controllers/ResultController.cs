using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PowerBall.Data;
using PowerBall.Migrations;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;

namespace PowerBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepo _resultrepo;

        public ResultController(IResultRepo drawrepo)
        {
            _resultrepo = drawrepo;
        }

        [HttpPost]
        [Route("Result-Top-Winners")]
        public async Task<IActionResult> WinnersTop(int DrawId)
        {
            return await _resultrepo.topwinners(DrawId);
        }


        [HttpPost]
        [Route("Result-All-Winners")]
        public IActionResult WinnersAll(int drawid)
        {
            return _resultrepo.WinnersAll(drawid);
        }

        [HttpPost]
        [Route("Result-By-UserID")]
        public IActionResult ResultByUserId(int userid)
        {
            return _resultrepo.ResultByUserId(userid);
        }
    }
}
