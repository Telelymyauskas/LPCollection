using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPC.Contracts.Database;

[Table("tbl_records", Schema = "public")]
public class Record
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("artist")]
    public string Artist { get; set; }

    [MaxLength(100)]
    [Column("album")]
    public string Album { get; set; }

    [Required]
    [Column("release_date")]
    public int CreatedAt { get; set; }

    [Required]
    [MaxLength(300)]
    [Column("img_url")]
    public string ImgURL { get; set; }
}