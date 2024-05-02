using System.ComponentModel.DataAnnotations.Schema;

namespace PowerBall.Models
{
    public class Games
    {
        public int Id { get; set; }
        public int GameNumber { get; set; }
        public string Values { get; set; }
        public int Price { get; set; }
        public string MatchingNumbers { get; set; } = "-";
        public double WinningAmount { get; set; } = 0;
        public int TicketID { get; set; }
        [ForeignKey("TicketID")]
        public Tickets Tickets { get; set; }
    }
}
