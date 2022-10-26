namespace lacuna_genetics
{
    public class RequestTokenBody
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RequestTokenBody() { }
    }

    public class RequestTokenResponse
    {
        public string? AccessToken { get; set; }
        public string Code { get; set; }
        public string? Message { get; set; }
    }
}
