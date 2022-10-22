namespace lacuna_genetics
{
    public class RequestJobResponse
    {
        public Job? Job { get; set; }
        public string Code { get; set; }
        public string? Message { get; set; }
    }

    public class Job
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string? Strand { get; set; }
        public string? StrandEncoded { get; set; }
        public string? GeneEncoded { get; set; }
    }
}
