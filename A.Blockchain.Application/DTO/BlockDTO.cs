using A.Blockchain.Application.Common;

namespace A.Blockchain.Application.DTO
{
    public class BlockDTO
    {
        public int Height { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public DateTime Timestamp { get; set; }

        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
