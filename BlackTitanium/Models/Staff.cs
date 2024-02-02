using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class Staff {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StaffId { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string MiddleName { get; set; } = string.Empty;

    public string FullName => $"{LastName} {FirstName} {MiddleName}";

    [ForeignKey(nameof(Department))]
    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}