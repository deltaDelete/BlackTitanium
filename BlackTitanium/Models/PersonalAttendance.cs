using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class PersonalAttendance {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersonalAttendanceId { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User? User { get; set; }

    public DateOnly Date { get; set; }
    
    [ForeignKey(nameof(ResponsibleStaff))]
    public int ResponsibleStaffId { get; set; }
    
    public Staff? ResponsibleStaff { get; set; }
}