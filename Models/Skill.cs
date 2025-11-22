using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mentor.Models
{
    [Table("TB_M_SKILL")]
    public class Skill
    {
        [Key]
        [Column("ID_SKILL")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("NM_SKILL")]
        public string Name { get; set; } = string.Empty;

        [Column("ID_USER")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
