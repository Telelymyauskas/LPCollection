using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPC.Contracts.Database;

[Table("tbl_library", Schema = "public")]
public class Library
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey(nameof(Record))]
    [Column("record_owned")]
    public int RecordOwned { get; set; }
    public Record Record { get; init; }
}