using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCG.AuditoriumBooking.api.Domain.Entities
{
    [Table("Bookings", Schema = "dbo")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AuditoriumId { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(TypeName = "time(0)")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }

        public int? Attendees { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedBy { get; set; }

        [StringLength(1000)]
        public string? RejectionReason { get; set; }

   
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [ForeignKey(nameof(AuditoriumId))]
        public virtual Auditorium? Auditorium { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User? Creator { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User? Modifier { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public virtual User? Approver { get; set; }
    }
}