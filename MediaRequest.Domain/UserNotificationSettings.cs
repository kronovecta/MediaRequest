namespace MediaRequest.Data.Notifications
{
    public class UserNotification
    {
        public int UserNotificationId { get; set; }
        public bool Enabled { get; set; }
        public string UserId { get; set; }
        public string WebhookURL { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string ProviderType { get; set; }
    }
}
