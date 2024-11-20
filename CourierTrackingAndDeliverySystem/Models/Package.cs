using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace CourierTrackingAndDeliverySystem.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TrackingNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string? SenderName { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ReceiverName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? ReceiverAddress { get; set; }

        [MaxLength(100)]
        public string CurrentStatus { get; set; } = "Awaiting Pickup";

        public DateTime? EstimatedDeliveryDate { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [ForeignKey("AssignedUser")]
        public int? AssignedTo { get; set; } // ID of the delivery personnel
        public User? AssignedUser { get; set; }
    }
    public enum PackageStatus
    {
        Pending,
        InTransit,
        Delivered,
        Delayed
    }

}
