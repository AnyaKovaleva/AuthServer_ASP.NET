using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Test.Models;

public class Secret
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    [Required] public ClassificationType Type { get; set; }
    public string Owner { get; set; }
    [Required] public string Description { get; set; }
}

public enum ClassificationType
{
    UNCLASSIFIED,
    CONFIDENTIAL,
    SECRET,
    TOP_SECRET
}