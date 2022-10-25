using System.Net.Http.Headers;

namespace lacuna_genetics
{
    internal class Program
    {

        private static HttpClient s_client = new HttpClient() { BaseAddress = new Uri("https://gene.lacuna.cc/") };

        internal static async Task Main(string[] args)
        {
            switch (args[0])
            {
                case "enroll":
                    await Enroll(args[1], args[2], args[3]);
                    break;
                case "requestToken":
                    await RequestToken(args[1], args[2]);
                    break;
                case "request":
                    await RequestJob(args[1], args[2]);
                    break;
                case "geneCheck":
                    var bolo = Checking.Gene(args[1], args[2]);
                    Console.WriteLine(bolo.ToString());
                    break;
            }


        }

        private static StringContent CreateHTTPPayload<T>(T input)
        {
            var serializedRequest = JsonExtensions.Serialize(input);
            var payload = new StringContent(serializedRequest, System.Text.Encoding.UTF8, "application/json");
            return payload;
        }

        private static async Task Enroll(string user, string email, string pass)
        {
            var requestBody = new EnrollBody
            {
                Username = user,
                Email = email,
                Password = pass

            };
            StringContent payload = CreateHTTPPayload(requestBody);
            var response = await s_client.PostAsync("api/users/create", payload);

            EnrollResponse enrollResponse = JsonExtensions.Deserialize<EnrollResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {enrollResponse.Code} \n Message: {enrollResponse.Message}");

        }

        private static async Task<string?> RequestToken(string user, string pass)
        {
            var requestBody = new RequestTokenBody { Username = user, Password = pass };
            StringContent payload = CreateHTTPPayload(requestBody);
            var response = await s_client.PostAsync("api/users/login", payload);
            RequestTokenResponse requestTokenResponse = JsonExtensions.Deserialize<RequestTokenResponse>(await response.Content.ReadAsStringAsync());

            if (requestTokenResponse.Code == "Success")
            {
                return requestTokenResponse.AccessToken;
            }
            else return null;
        }

        private static async Task RequestJob(string user, string pass)
        {
            var authToken = await RequestToken(user, pass);
            s_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = await s_client.GetStringAsync("api/dna/jobs");

            var requestJobResponse = JsonExtensions.Deserialize<RequestJobResponse>(response);

            if (requestJobResponse.Code == "Success")
            {
                Console.WriteLine(
                    $"Code: {requestJobResponse.Code}\n" +
                    $"Job id: {requestJobResponse.Job.Id}\n" +
                    $"Job type: {requestJobResponse.Job.Type}\n" +
                    $"Strand: {requestJobResponse.Job.Strand}\n" +
                    $"Strand Encoded: {requestJobResponse.Job.StrandEncoded}\n" +
                    $"Gene Encoded: {requestJobResponse.Job.GeneEncoded}\n"
                    );

                switch (requestJobResponse.Job.Type)
                {
                    case "EncodeStrand":
                        await EncodeStrand(requestJobResponse.Job.Id, requestJobResponse.Job.Strand);
                        break;
                    case "DecodeStrand":
                        await DecodeStrand(requestJobResponse.Job.Id, requestJobResponse.Job.StrandEncoded);
                        break;
                    case "CheckGene":
                        await CheckGene(requestJobResponse.Job.Id, requestJobResponse.Job.StrandEncoded, requestJobResponse.Job.GeneEncoded);
                        break;
                }

            }
        }

        private static async Task EncodeStrand(string jobId, string strand)
        {
            var strandEncoded = Encoding.StringToBase64(strand);

            var requestBody = new EncodeStrandBody { StrandEncoded = strandEncoded };

            StringContent payload = CreateHTTPPayload(requestBody);

            var response = await s_client.PostAsync($"api/dna/jobs/{jobId}/encode", payload);
            EncodeStrandResponse encodeStrandResponse = JsonExtensions.Deserialize<EncodeStrandResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {encodeStrandResponse.Code}\n" +
                $"Message: {encodeStrandResponse.Message}");
        }

        private static async Task DecodeStrand(string jobId, string strandEncoded)
        {
            var strandDecoded = Decoding.Base64ToString(strandEncoded);

            var requestBody = new DecodeStrandBody { Strand = strandDecoded };
            StringContent payload = CreateHTTPPayload(requestBody);

            var response = await s_client.PostAsync($"api/dna/jobs/{jobId}/decode", payload);

            DecodeStrandResponse decodeStrandResponse = JsonExtensions.Deserialize<DecodeStrandResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {decodeStrandResponse.Code}\n" +
                $"Message: {decodeStrandResponse.Message}");
        }

        private static async Task CheckGene(string jobId, string strandEncoded, string geneEncoded)
        {
            var strandDecoded = Decoding.Base64ToString(strandEncoded);
            var geneDecoded = Decoding.Base64ToString(geneEncoded);
            if(strandDecoded.Substring(0, 3) != "CAT")
            {
                strandDecoded = Converting.ComplementaryToTemplate(strandDecoded);
            }
            var geneIsActivated = Checking.Gene(strandDecoded, geneDecoded);

            var requestBody = new CheckGeneBody { IsActivated = geneIsActivated };

            StringContent payload = CreateHTTPPayload(requestBody);

            var response = await s_client.PostAsync($"api/dna/jobs/{jobId}/gene", payload);

            CheckGeneResponse checkGeneResponse = JsonExtensions.Deserialize<CheckGeneResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {checkGeneResponse.Code}\n" +
                $"Message: {checkGeneResponse.Message}\n");

        }

    }
}