using A.Blockchain.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface IBlockchainService
    {
        ResponseDTO<TransactionDTO> AddTransaction(RequestDTO<TransactionDTO> block);
        ResponseDTO<BlockDTO> GetLatestBlock();

        ResponseDTO<IEnumerable<BlockDTO>> GetAll();
        ResponseDTO<BlockDTO> Mine(RequestDTO<BlockDTO> block);

    }
}
