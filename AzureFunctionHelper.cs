using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory; // Nuget package of same name

namespace AzureUtilities
{
    public class AzureFunctionHelper
    {
        public static async Task<string> authAndCallFunction(
            string _tenant,
            string _serviceResourceId, // AppID of Azure Function
            string _clientId, // AAD Client ID
            string _appKey,   // AAD Client Secret
            string _functionURL)
        {
            // Get auth token and add the access token to the authorization header of the request.
            var httpClient = new HttpClient();
            var authContext = new AuthenticationContext(String.Format(System.Globalization.CultureInfo.InvariantCulture, "https://login.windows.net/{0}", _tenant));
            var clientCredential = new ClientCredential(_clientId, _appKey);
            AuthenticationResult result = await authContext.AcquireTokenAsync(_serviceResourceId, clientCredential);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            // Send the request and read the response
            HttpResponseMessage response = await httpClient.GetAsync(_functionURL);
            //var a = response.StatusCode;
            //var b = response.ReasonPhrase;
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> noAuthAndCallFunction(string _functionURL)
        {
            var httpClient = new HttpClient();

            // Send the request and read the response
            HttpResponseMessage response = await httpClient.GetAsync(_functionURL);
            //var a = response.StatusCode;
            //var b = response.ReasonPhrase;
            return await response.Content.ReadAsStringAsync();
        }
    }
}
