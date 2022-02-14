using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class ResponseDTO<T>
    {
        public ResponseDTO(string message, T data)
        {
            this.Message = message;
            this.Data = data;
        }

        public string Message { get; set; }
        public T Data { get; set; }
    }
}
