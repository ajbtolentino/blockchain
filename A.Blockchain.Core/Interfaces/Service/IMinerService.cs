using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface IMinerService
    {
        void Mine(BlockDTO block);
    }
}
