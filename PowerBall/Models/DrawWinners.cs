using System.ComponentModel.DataAnnotations.Schema;

namespace PowerBall.Models
{
    public class DrawWinners
    {
        public int Id { get; set; }
        public int DrawId { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string MatchingNumbers { get; set; }
        public int Rank { get; set; }
        public double WinningAmount { get; set; }
    }
}
