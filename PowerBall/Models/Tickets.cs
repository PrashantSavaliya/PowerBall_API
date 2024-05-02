using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PowerBall.Models
{
    public class Tickets
    {
        [Key]
        public int TicketID { get; set; }
        [JsonIgnore]
        public List<Games> Game { get; set; }
        public string BarCode { get; set; }
        public DateTime PlayerDateTime { get; set; }
        public int DrawId { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }

    }
}
