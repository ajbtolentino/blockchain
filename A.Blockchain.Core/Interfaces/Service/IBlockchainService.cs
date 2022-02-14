﻿using A.Blockchain.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface IBlockchainService
    {
        BlockDTO CreateGenesisBlock();

        BlockDTO GetLatestBlock();

        BlockDTO AddBlock(BlockDTO block);
    }
}
