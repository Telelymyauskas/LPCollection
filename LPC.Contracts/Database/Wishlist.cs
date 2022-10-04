using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPC.Contracts.Database;

[Table("tbl_wishlist", Schema = "public")]
public class Wishlist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey(nameof(Record))]
    [Column("record_wished")]
    public int RecordWished { get; set; }
    public Record Record { get; init; }
}