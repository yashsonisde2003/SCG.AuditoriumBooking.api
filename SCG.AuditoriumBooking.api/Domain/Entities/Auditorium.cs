using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCG.AuditoriumBooking.api.Domain.Entities
{
    [Table("Auditoriums", Schema = "dbo")]
    public class Auditorium
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditoriumId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Location { get; set; }

        public int? Capacity { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "datetime2(0)")]
        public DateTime CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime2(0)")]
        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User? Creator { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public virtual User? Modifier { get; set; }
    }
}