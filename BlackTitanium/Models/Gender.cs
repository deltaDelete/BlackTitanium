using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class Gender {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GenderId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}