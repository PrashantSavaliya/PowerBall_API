namespace PowerBall.Models
{
    public class DataList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TicketId { get; set; }
        public int DrawId { get; set; }
        public string BarCode { get; set; }
        public double TotalAmount { get; set; }
    }
}