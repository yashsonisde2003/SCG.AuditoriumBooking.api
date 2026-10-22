using System.ComponentModel.DataAnnotations;

namespace SCG.AuditoriumBooking.api.Application.Features.Authentication
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public string Password { get; set; } = string.Empty;
    }
}