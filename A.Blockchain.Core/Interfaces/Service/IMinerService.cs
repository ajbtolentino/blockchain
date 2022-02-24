using A.Blockchain.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface IMinerService
    {
        ResponseDTO<BlockDTO> Mine();
        ResponseDTO<BlockDTO> Mine(int[] transactionIds);
        ResponseDTO<BlockDTO> Mine(IEnumerable<TransactionDTO> transactions);
        ResponseDTO<BlockDTO> Mine(int count);

    }
}
