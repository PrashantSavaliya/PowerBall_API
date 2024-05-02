using Microsoft.AspNetCore.Mvc;
using PowerBall.Models;

namespace PowerBall.Repository.Interefaces
{
    public interface IAuthRepo
    {
        Task<IActionResult> Register(RegisterLogin registerUser);
        Task<IActionResult> Login(string MobileNo);
        string GenerateOtp();
    }
}
