using A.Blockchain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class ServiceBase
    {
        protected readonly IObjectMapper mapper;

        public ServiceBase(IObjectMapper mapper)
        {
            this.mapper = mapper;
        }

        protected TDestination Map<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }
    }
}
