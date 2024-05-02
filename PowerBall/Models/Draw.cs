using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerBall.Models
{
    public class Draw
    {
        [Key]
        public int DrawId { get; set; }
        public DateTime DrawDate { get; set; }
        public string FResult { get; set; }
        public double WinningAmount { get; set; }
        public bool HasWinner { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }
}
