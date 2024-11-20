using System.ComponentModel.DataAnnotations;

namespace CourierTrackingAndDeliverySystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; } // Store hashed passwords

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User"; // Role: User, DeliveryPersonnel, Admin

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
