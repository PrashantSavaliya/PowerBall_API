using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace PowerBall.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public AuthRepo(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<IActionResult> Register(RegisterLogin registerUser)
        {

            await _context.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            var response = new
            {
                Message = "User registered successfully",
                Data = new
                {
                    registerUser.UserId,
                    registerUser.FirstName,
                    registerUser.LastName,
                    registerUser.MobileNo,
                    registerUser.IsLoggedIn
                }
            };

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> Login(string MobileNo)
        {
            RegisterLogin registerLogin = _context.RegisterLogins.FirstOrDefault(x => x.MobileNo == MobileNo);

            if (registerLogin == null)
            {
                return new OkObjectResult("User is not found");
            }

            var token = GenerateToken(registerLogin);

            registerLogin.IsLoggedIn = true;

            _context.RegisterLogins.Update(registerLogin);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                token = token,
                Message = "User logged in successfully",
                UserId = registerLogin.UserId,
                Notice = "You can use UserId for play a game !!"
            });
        }

        public string GenerateOtp()
        {
            Random random = new();
            int otp = random.Next(1000, 10000);
            return otp.ToString();

        }

        private string GenerateToken(RegisterLogin registerLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UserId", registerLogin.UserId.ToString()), 
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims, 
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}