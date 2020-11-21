namespace MediaRequest.Domain
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public bool Status { get; set; }
    }
}
