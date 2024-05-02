using Twilio.Clients;
using Twilio.Http;

namespace PowerBall.Repository
{
    public class TwilioClient : ITwilioRestClient
    {
        private readonly TwilioRestClient _client;
        public TwilioClient(IConfiguration config, System.Net.Http.HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = new TwilioRestClient(
                config["Twilio:AccountSID"],
                config["Twilio:AuthToken"],
                httpClient : new SystemNetHttpClient(httpClient));
        }
        public string AccountSid => _client.AccountSid;

        public string Region => _client.Region;

        public global::Twilio.Http.HttpClient HttpClient => _client.HttpClient;

        public Response Request(Request request) => _client.Request(request);

        public Task<Response> RequestAsync(Request request) => _client.RequestAsync(request);
    }
}
