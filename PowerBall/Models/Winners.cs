namespace PowerBall.Models
{
    public class Winners
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public int GameNumber { get; set; }
        public string YourNumbers { get; set; }
        public string ResultNumbers { get; set; }
        public string MatchedNumbers { get; set; }
        public double TotalEarning { get; set; }
    }
}
