namespace lacuna_genetics
{
    public class EnrollBody
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public EnrollBody() { }
    }

    public class EnrollResponse
    {
        public string Code { get; set; }

        public string? Message { get; set; }

        public EnrollResponse() { }
    }
}
