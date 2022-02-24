using Grpc.Core;

namespace A.Blockchain.Node.Services
{
    public class NodeService : Node.NodeBase
    {
        public override Task<AddBlockReply> AddBlock(AddBlockRequest request, ServerCallContext context)
        {
            return base.AddBlock(request, context);
        }
    }
}
