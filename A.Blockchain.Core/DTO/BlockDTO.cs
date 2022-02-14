﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class BlockDTO : DTOBase
    {
        public BlockDTO(string hash, string previousHash, IEnumerable<TransactionDTO>? transactions = null)
        {
            this.Hash = hash;
            this.PreviousHash = previousHash;
            this.Transactions = transactions ?? new List<TransactionDTO>();
        }

        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public string Data { get; set; } = string.Empty;
        public int Nonce { get; set; }
        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
