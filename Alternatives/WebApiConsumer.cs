using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Alternatives.CustomExceptions;

namespace Alternatives
{
    public class WebApiConsumer
    {
        private string _baseUrl;
        private const string JSON_FORMATTER = "application/json";

        private const string RESPONSE_IS_NULL = "Response is null",
                             RESPONSE_STATUS_IS_NOT_SUCCESS = "Response status is not success",
                             GET_OPERATION_IS_FAILED = "Get operation is failed";

        public WebApiConsumer(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public TRes Post<TRes, TReq>(string innerUrl, TReq myRequest = default(TReq))
        {
            string serializedContent = myRequest.Serialize();
            StringContent content = new StringContent(serializedContent);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(JSON_FORMATTER);


            return Post<TRes>(innerUrl, content, new MediaTypeWithQualityHeaderValue(JSON_FORMATTER));
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public TRes Post<TRes>(string innerUrl, HttpContent httpContent,
                               params MediaTypeWithQualityHeaderValue[] clientRequestMediaTypes)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                foreach (MediaTypeWithQualityHeaderValue mediaType in clientRequestMediaTypes)
                    client.DefaultRequestHeaders.Accept.Add(mediaType);

                response = client.PostAsync(innerUrl, httpContent).Result;
            }

            if (response == null)
                throw new FriendlyException(RESPONSE_IS_NULL);
            else if (!response.IsSuccessStatusCode)
                throw new FriendlyException($"{RESPONSE_STATUS_IS_NOT_SUCCESS} : {response.ReasonPhrase}");

            TRes responseItem = (response.Content.ReadAsStringAsync().Result).Deserialize<TRes>();
            return responseItem;
        }

        public string Get(string innerUrl, string myRequest = null)
        {
            return Get(innerUrl, myRequest, new MediaTypeWithQualityHeaderValue(JSON_FORMATTER));
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public string Get(string innerUrl, string myRequest,
                          params MediaTypeWithQualityHeaderValue[] clientRequestMediaTypes)
        {
            if (!string.IsNullOrEmpty(myRequest))
                innerUrl += myRequest;

            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                foreach (MediaTypeWithQualityHeaderValue mediaType in clientRequestMediaTypes)
                    client.DefaultRequestHeaders.Accept.Add(mediaType);

                try
                {
                    response = client.GetAsync(innerUrl).Result;
                }
                catch (Exception ex)
                {
                    throw new FriendlyException(GET_OPERATION_IS_FAILED, ex);
                }
            }

            if (response == null)
                throw new FriendlyException(RESPONSE_IS_NULL);
            if (!response.IsSuccessStatusCode)
                throw new FriendlyException($"{RESPONSE_STATUS_IS_NOT_SUCCESS} : {response.ReasonPhrase}");

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public static bool CheckNet()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public bool CheckConnection()
        {
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(_baseUrl);
                request.Timeout = 5000;
                request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse) request.GetResponse();

                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}