namespace Deneme.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public bool IsRead { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
