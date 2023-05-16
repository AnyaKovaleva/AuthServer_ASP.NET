using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Test.Models;

public class User
{
    [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Key]
    [Required]
    [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
    public string Email { get; set; }

    [Required] public string Password { get; set; }

}