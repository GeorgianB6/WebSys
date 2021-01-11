using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Facebook_targeting;
using Newtonsoft.Json;


public interface IFacebookClient
{
    Task DeleteAsync(string accessToken, string endpoint, object payload, string args = null);
    Task PostAsync(string accessToken, string endpoint, object payload, string args = null);
}

public class FacebookClient : IFacebookClient
{
    private readonly HttpClient _httpClient;

    public FacebookClient(string endpoint)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(endpoint)
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    
    public async Task DeleteAsync(string accessToken, string endpoint, object data, string args = null)
    {
        var json = JsonConvert.SerializeObject(data);
        
        var response = await _httpClient.DeleteAsync($"{endpoint}?access_token={accessToken}&payload={json}");
        var stringContent = await response.Content.ReadAsStringAsync();
        if (stringContent.Contains("error"))
        {
            var facebookResponse = JsonConvert.DeserializeObject<FacebookResponse.RootObject>(stringContent);
            throw new Exception(facebookResponse.error.ToString());
        }
    }

    public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
    {
        var payload = FacebookUtils.GetPayload(data, accessToken);
        var response = await _httpClient.PostAsync(endpoint, payload);
        var stringContent = await response.Content.ReadAsStringAsync();
    }
}