using A.Blockchain.Application.Common;

namespace A.Blockchain.Application.DTO
{
    public class TransactionDTO
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
