using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPC.Contracts.Database;

[Table("tbl_owned_records", Schema = "public")]
public class OwnedRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey(nameof(Record))]
    [Column("record_owned")]
    public int RecordWished { get; set; }
}