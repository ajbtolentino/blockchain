using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class RequestDTO<T>
    {
        public RequestDTO(T data, string requestedBy, DateTime requestedDate)
        {
            this.Data = data;
            this.RequestedDate = requestedDate;
            this.RequestedBy = requestedBy;
        }

        public T Data { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedBy { get; set; }
    }
}
