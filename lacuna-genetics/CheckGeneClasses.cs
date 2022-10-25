namespace lacuna_genetics
{
    public class CheckGeneBody
    {
        public bool IsActivated { get; set; }
        public CheckGeneBody() { }
    }

    public class CheckGeneResponse
    {
        public string Code { get; set; }
        public string? Message { get; set; }
    }
}
