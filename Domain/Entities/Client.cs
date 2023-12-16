namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
