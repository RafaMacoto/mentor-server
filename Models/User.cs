using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mentor.Models
{
    [Table("TB_M_USER")]
    public class User
    {
        [Key]
        [Column("ID_USER")]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("NM_FULLNAME")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [Column("DS_EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("DS_PASSWORD")]
        public string Password { get; set; } = string.Empty;

        [Column("CAREER_GOAL")]
        public string CareerGoal { get; set; } = string.Empty;


        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
