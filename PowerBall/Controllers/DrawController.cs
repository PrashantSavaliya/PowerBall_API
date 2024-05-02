using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace PowerBall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawRepo _drawrepo;
        private readonly IConfiguration _configuration;

        public DrawController(IDrawRepo drawrepo = null, IConfiguration configuration = null)
        {
            _drawrepo = drawrepo;
            _configuration = configuration;
        }




        [HttpPost]
        [Route("Generate-DrawResult-ByAdmin")]
        public async Task<IActionResult> DrawResult(string admin, string password, int drawid)
        {
            string admindb = ConvertToHex(_configuration["AdminAccess:Admin"]);
            string passdb = ConvertToHex(_configuration["AdminAccess:Password"]);

            if ((ConvertToHex(admin) != admindb) && (ConvertToHex(password) != passdb))
            {
                return BadRequest("Only Admin can make the result ");
            }

            return await _drawrepo.Draw(drawid);
        }

        private static string ConvertToHex(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                StringBuilder hexStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hexStringBuilder.Append(b.ToString("x2"));
                }

                return hexStringBuilder.ToString();
            }
        }

    }
}
