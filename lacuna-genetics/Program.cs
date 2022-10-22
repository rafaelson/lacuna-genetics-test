using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;

namespace lacuna_genetics
{
    internal class Program
    {

        internal static HttpClient s_client = new HttpClient() { BaseAddress = new Uri("https://gene.lacuna.cc/") };

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
            }


        }

        internal static StringContent CreateHTTPPayload<T>(T input)
        {
            var serializedRequest = JsonExtensions.Serialize(input);
            var payload = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            return payload;
        }

        internal static async Task Enroll(string user, string email, string pass)
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

        internal static async Task<string?> RequestToken(string user, string pass)
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

        internal static async Task RequestJob(string user, string pass)
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
                    $"Gene Encoded: {requestJobResponse.Job.GeneEncoded}"
                    );

                switch (requestJobResponse.Job.Type)
                {
                    case "EncodeStrand":
                        EncodeStrand(requestJobResponse.Job.Id, requestJobResponse.Job.Strand);
                        break;
                }

            }
        }

        private static async Task EncodeStrand(string jobId, string strand)
        {
            var encodedStrand = Conversion.StringToBase64(strand);

            var requestBody = new EncodeStrandBody { StrandEncoded = encodedStrand };

            StringContent payload = CreateHTTPPayload(requestBody);

            var response = await s_client.PostAsync($"api/dna/jobs/{jobId}/encode", payload);
            EncodeStrandResponse encodeStrandResponse = JsonExtensions.Deserialize<EncodeStrandResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {encodeStrandResponse.Code}\n" +
                $"Message: {encodeStrandResponse.Message}");
        }

    }
}