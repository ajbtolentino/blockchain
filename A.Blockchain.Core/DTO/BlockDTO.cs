﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class BlockDTO : DTOBase
    {
        public int Height { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public DateTime Timestamp { get; set; }

        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
