using A.Blockchain.Core.Interfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace A.Blockchain.Service.Tests
{
    [TestClass]
    public class BlockchainServiceTests
    {
        [TestMethod]
        public void CreateGenesisBlock()
        {
            var blockchainRepositoryMock = new Mock<IBlockchainRepository>();
            var blockchainService = new BlockchainService(blockchainRepositoryMock.Object);

            blockchainService.CreateGenesisBlock();
        }
    }
}