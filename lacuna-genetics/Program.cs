using System.Text;
using System.Text.Json;

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
            }


        }

        internal static StringContent CreateHTTPPayload<T>(T input)
        {
            var serializedRequest = JsonSerializer.Serialize(input);
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

            EnrollResponse enrollResponse = JsonSerializer.Deserialize<EnrollResponse>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Code: {enrollResponse.Code} \n Message: {enrollResponse.Message}");

        }

        internal static async Task RequestToken(string user, string pass)
        {
            var requestBody = new RequestTokenBody { Username = user, Password = pass };
            StringContent payload = CreateHTTPPayload(requestBody);
            var response = await s_client.PostAsync("api/users/login", payload);

            RequestTokenResponse requestTokenResponse = JsonSerializer.Deserialize<RequestTokenResponse>(await response.Content.ReadAsStringAsync());
            Console.WriteLine($"Access Token: {requestTokenResponse.AccessToken} \n" +
                $"Code: {requestTokenResponse.Code} \n" +
                $"Message: {requestTokenResponse.Message}");
        }

    }
}