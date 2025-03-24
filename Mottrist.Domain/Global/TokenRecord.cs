namespace Mottrist.Domain.Global
{
    public class TokenRecord
    {
        public string Token { get; set; }
        public string IPAddress { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; }
    }
}
