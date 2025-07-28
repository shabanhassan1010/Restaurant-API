namespace Restaurant.Application.Restaurants.DTOS.Account.Read
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expireon { get; set; }
        public List<string> Roles { get; set; }
    }
}
