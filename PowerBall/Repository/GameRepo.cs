using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;

namespace PowerBall.Repository
{
    public class GameRepo : IGameRepo
    {
        private readonly DataContext _context;

        public GameRepo(DataContext context, IDistributedCache cache)
        {
            _context = context;
        }

        private IActionResult ValidUser(int userId)
        {
            var user = _context.RegisterLogins.FirstOrDefault(x => x.UserId == userId);

            if (user == null) return new BadRequestObjectResult("You are not registered  , First You have to registered in brother !!");

            if (user.IsLoggedIn == false) return new BadRequestObjectResult("You are not logged in , First You have to logged in brother !!");

            return new OkObjectResult("You can play a game my brother");
        }


        public async Task<IActionResult> PlayGame(int userid, int GameCount)
        {
            if ( GameCount == 0)
            {
                return new BadRequestObjectResult("Invalid input. Please provide valid values for GameCount.");
            }

            // Check if the user is valid
            IActionResult validUserResult = ValidUser(userid);

            if (validUserResult is OkObjectResult)
            {
                var drawdata = await _context.Draw.FirstOrDefaultAsync(x => x.IsActive == true);
                List<Games> games = new List<Games>();

                var tickets = new Tickets
                {
                    BarCode = GenerateRandomBarcode(12),
                    DrawId = drawdata.DrawId,
                    PlayerDateTime = DateTime.Today,
                    UserId = userid
                };

                tickets.TotalAmount = GameCount * 10;

                await _context.AddAsync(tickets);
               await _context.SaveChangesAsync();

                for (int i = 1; i <= GameCount; i++)
                {
                    var arr = GenerateRandomArray();
                    var game = new Games
                    {
                        GameNumber = i,
                        Values = string.Join(", ", arr),
                        Price = 10,
                        TicketID = tickets.TicketID,
                    };

                    await _context.Game.AddAsync(game);
                    await _context.SaveChangesAsync();

                    games.Add(game);
                }

                return new OkObjectResult(new
                {
                    Message = "This is your games and your Lucky keys !! Best Of Luck !!",
                    Data = games,
                    Information = "You can check your result by your UserId !!"
                });
            }
            else
            {
                return new BadRequestObjectResult("Invalid player. Please enter the right UserId!!");
            }
        }


        private string GenerateRandomArray()
        {
            Random random = new Random();
            HashSet<int> generatedValues = new HashSet<int>();
            int[] randomArray = new int[6];

            for (int i = 0; i < 6; i++)
            {
                int randomValue;
                do
                {
                    randomValue = random.Next(1, 45 + 1);
                } while (generatedValues.Contains(randomValue));

                generatedValues.Add(randomValue);
                randomArray[i] = randomValue;
            }

            string randomString = string.Join(", ", randomArray);
            return randomString;
        }
        private string GenerateRandomBarcode(int length)
        {
            const string characters = "0123456789";
            Random random = new Random();
            char[] couponArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                couponArray[i] = characters[random.Next(characters.Length)];
            }

            return new string(couponArray);
        }
    }
}
