using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerBall.Repository.Interefaces;
using System.Diagnostics;

namespace PowerBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataListController : ControllerBase
    {
        private readonly IDataListRepo _repo;
        public DataListController(IDataListRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("EF-GetAllData")]
        public async Task<IActionResult> GetAllData()
        {
            var stopwatch = Stopwatch.StartNew(); 

            var data = await _repo.GetAllData();

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed.TotalSeconds; 

            return Ok(new
            {
                elapsedTime = elapsedTime,
                data = data
            });
        }

        [HttpGet("EF-GetDataByPage")]
        public async Task<IActionResult> GetDataByPage(int pageIndex = 1, int pageSize = 50)
        {
            var stopwatch = Stopwatch.StartNew(); 

            var data = await _repo.GetDataByPage(pageIndex, pageSize);

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed.TotalSeconds; 

            return Ok(new
            {
                elapsedTime = elapsedTime,
                data = data
            });
        }

        [HttpGet("SP-GetAllData")]
        public async Task<IActionResult> GetDataBySP()
        {
            var stopwatch = Stopwatch.StartNew(); 

            var data = await _repo.GetDataBY_SP();

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed.TotalSeconds; 

            return Ok(new
            {
                elapsedTime = elapsedTime,
                data = data
            });
        }

        [HttpGet("SP-GetDataByPage")]
        public async Task<IActionResult> GetDataByPageSP(int pageIndex = 1, int pageSize = 50)
        {
            var stopwatch = Stopwatch.StartNew();

            var data = await _repo.GetDataByPageSP(pageIndex, pageSize);

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed.TotalSeconds;

            return Ok(new
            {
                elapsedTime = elapsedTime,
                data = data
            });
        }
    }
}
