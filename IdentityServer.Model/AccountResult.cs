namespace IdentityServer.Model
{
    public class AccountResult
    {
        public string Status { get; set; }
        public UserInfo User { get; set; }
        public string Message { get; set; }
    }
}
