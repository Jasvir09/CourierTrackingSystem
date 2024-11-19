namespace CourierTrackingandDeliverySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordPasswordHash { get; set; }
        public string PasswordHash { get; internal set; }
    }
}
