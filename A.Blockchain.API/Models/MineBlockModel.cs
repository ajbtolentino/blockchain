namespace A.Blockchain.API.Models
{
    public class MineBlockModel
    {
        public string PreviousHash { get; set; }

        public int[] TransactionIds { get; set; }
    }
}
