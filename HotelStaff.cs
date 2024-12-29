
using System.ComponentModel.DataAnnotations;
public class HotelStaff
{
    [Key]
    public int StaffId { get; set; }
    public string Name { get; set; } // Non-nullable
    public string Role { get; set; } // Non-nullable

    public HotelStaff(int staffId, string name, string role)
    {
        StaffId = staffId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
