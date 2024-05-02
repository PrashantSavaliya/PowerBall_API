using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PowerBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _auth;
        private readonly ITwilioRestClient _client;

        public AuthController(IAuthRepo context, ITwilioRestClient client)
        {
            _auth = context;
            _client = client;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterLogin registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter the correct values in the fields");
            }
            return await _auth.Register(registerUser);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string MobileNo, string OTP)
        {
            var mo = HttpContext.Session.GetString("Mobileno");
            var generatedOtp = HttpContext.Session.GetString("GeneratedOtp");

            if (generatedOtp != OTP && mo != MobileNo)
            {
                return BadRequest("Please generate otp first !");
            }

            try
            {
                MessageResource.Create(
                      to: new PhoneNumber($"+91{MobileNo}"),
                      from: new PhoneNumber("+15866844668"),
                      body: $"Welcome to PowerBall Draw and you logged In at {DateTime.Now}  !!",
                      client: _client);
                return await _auth.Login(MobileNo);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error occurred while sending Twilio message: {ex.Message}");
                return await _auth.Login(MobileNo);
            }


        }

        [HttpGet]
        [Route("Generate-OTP")]
        public IActionResult generateOTP(string mobileno)
        {
            var generatedOtp = _auth.GenerateOtp();
            HttpContext.Session.SetString("GeneratedOtp", generatedOtp);
            HttpContext.Session.SetString("Mobileno", mobileno);

            return Ok($"Your OTP is {generatedOtp}  !! \n Generated OTP is validated only 60 seconds !!");
        }
    }
}