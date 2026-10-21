namespace SCG.AuditoriumBooking.api.Application.Features.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string RoleType { get; set; } = string.Empty;
    }
}