namespace TransactionSyncAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        public User CreatedByUser { get; set; }
    }
}
