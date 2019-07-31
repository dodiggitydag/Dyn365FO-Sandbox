/*
You must have a Cognitive Services API account with access to the Bing Spell Check API. If you don't have an Azure subscription, you can create an account for free. Before continuing, You will need the access key provided after activating your free trial, or a paid subscription key from your Azure dashboard.
https://docs.microsoft.com/en-us/azure/cognitive-services/bing-spell-check/quickstarts/csharp
https://docs.microsoft.com/azure/cognitive-services/cognitive-services-apis-create-account
https://azure.microsoft.com/try/cognitive-services/?api=text-analytics
1,000 calls per month are free
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BingSpellCheckAPIWrapper
{
    public class BingSpellCheckAPIWrapper
    {
        //private static string s_host = "https://api.cognitive.microsoft.com";
        private static string s_host = "https://YOUR_ENDPOINT_ADDRESS.cognitiveservices.azure.com"; // Found specific endpoint in Azure Portal
        private static string s_path = "/bing/v7.0/SpellCheck?";
        private static string s_params = "mkt=en-US&mode=spell";
        private string m_key = "YOUR API KEY HERE"; // Found specific endpoint in Azure Portal
        private string m_response;

        public BingSpellCheckAPIWrapper()
        {
        }

        public BingSpellCheckAPIWrapper(string _apiKey)
        {
            m_key = _apiKey;
        }

        public string parmApiKey(string _apiKey)
        {
            m_key = _apiKey;
            return m_key;
        }

        public string SpellCheck(string _textToCheck)
        {
            this.DoSpellCheck(_textToCheck);

            // Need to wait so that the call returns, and the class variable is populated (this is a hack, we should wait for a Task)
            System.Threading.Thread.Sleep(3000);

            return m_response;
        }

        private async void DoSpellCheck(string _textToCheck)
        {
            //string returnValue;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", m_key);

            HttpResponseMessage response = new HttpResponseMessage();

            string uri = s_host + s_path + s_params;

            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("text", _textToCheck));

            using (FormUrlEncodedContent content = new FormUrlEncodedContent(values))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                response = await client.PostAsync(uri, content);
            }

            string contentString = await response.Content.ReadAsStringAsync();

            this.m_response = contentString;
        }

    }
}
