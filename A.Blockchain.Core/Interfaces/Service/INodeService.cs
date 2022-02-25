using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.DTO.Transaction;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface INodeService
    {
        ResponseDTO<BlockDTO> AddBlock(BlockDTO block);

        ResponseDTO<bool> DeleteTransactions(params int[] transactions);

        ResponseDTO<BlockDTO> GetLatestBlock();

        ResponseDTO<IEnumerable<BlockDTO>> GetAllBlocks();

        ResponseDTO<IEnumerable<TransactionDTO>> GetPendingTransactions();
    }
}
