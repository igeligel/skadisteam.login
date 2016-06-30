using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using skadisteam.login.Constants;
using skadisteam.login.Http.Headers;
using HttpMethod = skadisteam.login.Http.HttpMethod;

namespace skadisteam.login.Factories
{
    public static class RequestFactory
    {
        public static HttpResponseMessage Create(HttpMethod method, Uri uri,
            string path, Accept acceptHeader, string acceptLanguage,
            bool upgradeInsecureRequests, bool steamCommunityOriginSet,
            bool steamCommunityRefererSet, bool isXmlHttpRequest,
            bool cacheControlSet,
            IEnumerable<KeyValuePair<string, string>> postContent,
            CookieContainer cookieContainer)
        {
            HttpResponseMessage response = null;
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Add(HttpHeaderKeys.AcceptEncoding, "gzip, deflate");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", acceptLanguage);
                client.DefaultRequestHeaders.Add(HttpHeaderKeys.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
                client.DefaultRequestHeaders.Host = uri.Host;

                if (acceptHeader == Accept.All)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "*/*");
                }
                else if (acceptHeader == Accept.Html)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                }

                if (steamCommunityOriginSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.Origin, Ressources.SteamCommmunitySecureBase);
                }

                if (cacheControlSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.CacheControl, "max-age=0");
                }

                if (isXmlHttpRequest)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.XRequestedWith, "XMLHttpRequest");
                }

                if (steamCommunityRefererSet)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.Referer, "https://steamcommunity.com/login/home/?goto=0");
                }

                if (upgradeInsecureRequests)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaderKeys.UpgradeInsecureRequests, "1");
                }

                if (method == HttpMethod.POST)
                {
                    var content = new FormUrlEncodedContent(postContent);
                    response = client.PostAsync(path, content).Result;
                }
                else if (method == HttpMethod.GET)
                {
                    response = client.GetAsync(path).Result;
                }

            }
            return response;
        }
    }
}
