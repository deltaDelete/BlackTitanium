using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class User {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string MiddleName { get; set; } = string.Empty;

    public string FullName => $"{LastName} {FirstName} {MiddleName}";

    [ForeignKey(nameof(Gender))]
    public int GenderId { get; set; }

    public Gender? Gender { get; set; }

    public ulong Phone { get; set; }

    public DateOnly Birthday { get; set; }
    public DateOnly PassportDate { get; set; }

    [Range(0000, 9999)]
    public int PassportSeries { get; set; }

    [Range(000000, 999999)]
    public int PassportNumber { get; set; }

    [MaxLength(255)]
    public string Login { get; set; } = string.Empty;

    [MaxLength(8)]
    public string PasswordSalt { get; set; } = string.Empty;

    [MinLength(64), MaxLength(64)]
    public string PasswordHash { get; set; } = string.Empty;
}