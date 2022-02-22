namespace A.Blockchain.API.Models
{
    public class SendModel
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
    }
}
