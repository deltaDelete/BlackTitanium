using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class Group {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupId { get; set; }

    [ForeignKey(nameof(ResponsibleStaff))]
    public int ResponsibleStaffId { get; set; }

    public Staff? ResponsibleStaff { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
}