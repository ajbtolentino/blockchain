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
        ResponseDTO<bool> CreateGenesisBlock();

        ResponseDTO<BlockDTO> GetLatestBlock();

        ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> block);

        ResponseDTO<IEnumerable<BlockDTO>> GetAll();
    }
}
