using Microsoft.AspNetCore.Mvc;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;

namespace PowerBall.Repository
{
    public class ResultRepo : IResultRepo
    {
        private readonly DataContext _context;
        public ResultRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> topwinners(int DrawId)
        {
            DrawWinners drawWinners = new DrawWinners();
            var check = _context.DrawWinners.Where(x => x.DrawId == DrawId).ToList();
            if (check.Count == 0)
            {
                var ticketdata = _context.Tickets.Where(x => x.DrawId == DrawId).ToList();
                for (int i = 0; i < ticketdata.Count; i++)
                {
                    var gamedata = _context.Game.Where(x => x.TicketID == ticketdata[i].TicketID).ToList();
                    for (int j = 0; j < gamedata.Count; j++)
                    {
                        int cc = gamedata[j].MatchingNumbers.ToString().Split(',').Length;
                        if (cc > 4)
                        {
                            drawWinners.MatchingNumbers = gamedata[j].MatchingNumbers;
                            drawWinners.WinningAmount = gamedata[j].WinningAmount;
                            drawWinners.TicketId = gamedata[j].TicketID;
                            drawWinners.UserId = ticketdata[i].UserId;
                            drawWinners.DrawId = DrawId;
                            drawWinners.Rank = 1;

                            await _context.DrawWinners.AddAsync(drawWinners);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return new OkObjectResult(new
                {
                    Winners = (drawWinners != null) ? $"No one is got 5 or 6 matching numbers in draw {DrawId} winner" : $"{drawWinners}",
                });
            }
            else
            {
                return new OkObjectResult(check);
            }
        }

        public IActionResult WinnersAll(int drawid)
        {
            var sentence = $"All winners of DrawId {drawid} is : \n\n";
            var ticketdata = _context.Tickets.Where(x => x.DrawId == drawid).ToList();

            if (ticketdata.Count == 0) return new OkObjectResult("Please enter correct DrawId");

            for (int i = 0; i < ticketdata.Count; i++)
            {
                var drawdata = _context.Draw.FirstOrDefault(x => x.DrawId == ticketdata[i].DrawId);
                sentence += $"TicketId = {ticketdata[i].TicketID} \n";
                var gamedata = _context.Game.Where(x => x.TicketID == ticketdata[i].TicketID).ToList();
                for (int j = 0; j < gamedata.Count; j++)
                {
                    sentence += $"You GameNumber is {j + 1}  " +
                                 $"And value is [{gamedata[j].Values}] " +
                                 $"Matching this Draw Result value [{drawdata.FResult}] " +
                                 $"Your Matching string is [{gamedata[j].MatchingNumbers}] " +
                                 $"Your winning amount is {gamedata[j].WinningAmount} . \n";
                }
            }
            return new OkObjectResult(sentence);
        }

        public IActionResult ResultByUserId(int userid)
        {
            var userdata = _context.RegisterLogins.FirstOrDefault(x => x.UserId == userid);
            var sentence = $"Welcome {userdata.FirstName} {userdata.LastName} ,\n";

            var ticketdata = _context.Tickets.Where(x => x.UserId == userid).ToList();
            for (int i = 0; i < ticketdata.Count; i++)
            {
                var drawdata = _context.Draw.FirstOrDefault(x => x.DrawId == ticketdata[i].DrawId);
                sentence += $"Your TicketId = {ticketdata[i].TicketID} \n";
                var gamedata = _context.Game.Where(x => x.TicketID == ticketdata[i].TicketID).ToList();
                for (int j = 0; j < gamedata.Count; j++)
                {
                    sentence += $"You played {j + 1} games " +
                                $"Your value is [{gamedata[j].Values}] " +
                                $"Matching this Draw Result value [{drawdata.FResult}] " +
                                $"Your Matching string is [{gamedata[j].MatchingNumbers}] " +
                                $"Your winning amount is {gamedata[j].WinningAmount} . \n";
                }
            }
            return new OkObjectResult(sentence);
        }
    }
}
