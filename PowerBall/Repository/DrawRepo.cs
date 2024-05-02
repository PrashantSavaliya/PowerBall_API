using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerBall.Data;
using PowerBall.Models;
using PowerBall.Repository.Interefaces;

namespace PowerBall.Repository
{
    public class DrawRepo : IDrawRepo
    {
        private readonly DataContext _context;

        public DrawRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Draw(int drawid)
        {
            var drawdata = _context.Draw.FirstOrDefault(x => x.DrawId == drawid);
            var drawdata2 = _context.Draw.OrderByDescending(x => x.DrawId).FirstOrDefault();
            if (drawdata != null)
            {
                drawdata.IsActive = IsExactlySevenDaysAgo(drawdata.DrawDate);

                if (drawdata.IsActive == false && drawdata.DrawDate <= DateTime.Today)
                {
                    var randomarray = GenerateRandomArray();
                    if (drawdata.FResult == "-")
                    {
                        drawdata.FResult = randomarray;
                    }
                }
                drawdata.HasWinner = comparisions(drawdata.DrawId, drawdata.FResult);
                if (drawdata.HasWinner == true)
                {
                    drawdata2.WinningAmount = 10000000;
                    _context.Draw.Update(drawdata2);
                }
                else
                {
                    _context.Draw.Update(drawdata);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                var draw = new Draw
                {
                    DrawDate = drawdata2.DrawDate.AddDays(7),
                    FResult = "-",
                    WinningAmount = 10000000,
                    IsActive = false
                };

                await _context.Draw.AddAsync(draw);
                await _context.SaveChangesAsync();

                return new OkObjectResult(draw);
            }
            return new OkObjectResult(drawdata);
        }

        private bool comparisions(int drawid, string randomvalue)
        {
            bool Islength4 = false;
            var sentense = "";
            List<string> matchingStrings = new List<string>();
            string[] resultdatastring = randomvalue.ToString().Split(',');
            var ticketData = _context.Tickets.Where(x => x.DrawId == drawid).ToList();
            for (int i = 0; i < ticketData.Count; i++)
            {
                var gamedata = _context.Game.Where(x => x.TicketID == ticketData[i].TicketID).ToList();

                for (int j = 0; j < gamedata.Count; j++)
                {
                    string[] valuedatastring = gamedata[j].Values.ToString().Split(',');

                    for (int k = 0; k < resultdatastring.Length; k++)
                    {
                        string rd = resultdatastring[k].Trim();

                        for (int l = 0; l < valuedatastring.Length; l++)
                        {
                            string vd = valuedatastring[l].Trim();
                            if (rd == vd)
                            {
                                matchingStrings.Add(vd);
                            }
                        }
                    }
                    if (matchingStrings.Count > 0)
                    {
                        sentense += $"In GameNumber {i + 1}" +
                                 $" Your Numbers  [{gamedata[j].Values}] is matched to the " +
                                 $"result Numbers [{randomvalue}]  " +
                                 $" And matched Number is [{string.Join(", ", matchingStrings)}].\n";

                        if (matchingStrings.Count > 4) Islength4 = true;

                        gamedata[j].MatchingNumbers = string.Join(", ", matchingStrings);
                        gamedata[j].WinningAmount = Math.Pow(10, (matchingStrings.Count) + 1);

                        _context.Game.Update(gamedata[j]);
                        _context.SaveChanges();

                        matchingStrings.Clear();
                    }
                }
            }

            if (Islength4 == true) return true;
            return false;
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
        private bool IsExactlySevenDaysAgo(DateTime givenDate)
        {
            DateTime currentDate = DateTime.Today;
            DateTime sevenDaysAgo = givenDate.AddDays(-6);

            return (sevenDaysAgo <= currentDate && givenDate > currentDate);
        }
    }
}
