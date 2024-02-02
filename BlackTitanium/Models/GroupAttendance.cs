using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackTitanium.Models;

public class GroupAttendance {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupAttendaceId { get; set; }
    
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User? User { get; set; }
    
    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }

    public Group? Group { get; set; }
}