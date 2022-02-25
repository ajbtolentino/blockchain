using A.Blockchain.Core.DTO.Transaction;

namespace A.Blockchain.Core.DTO.Block
{
    public class BlockDTO : DTOBase
    {
        public int Height { get; set; }
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; } = string.Empty;
        public string PreviousHash { get; set; } = string.Empty;
        public int Nonce { get; set; }

        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
