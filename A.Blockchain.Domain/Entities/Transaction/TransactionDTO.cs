namespace A.Blockchain.Core.DTO.Transaction
{
    public class TransactionDTO : DTOBase
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
