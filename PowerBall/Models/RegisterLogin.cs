using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PowerBall.Models
{
    public class RegisterLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 10 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(3), MaxLength(10)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string MobileNo { get; set; }

        [JsonIgnore]
        public bool IsLoggedIn { get; set; }

    }
}
