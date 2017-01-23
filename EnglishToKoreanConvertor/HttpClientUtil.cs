using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishToKoreanConvertor
{
    public static class HttpClientUtil
    {
        public static string requestByGet(String url, Dictionary<string, string> querys)
        {
            string requestUrl = HttpClientUtil.makeUrl(url, querys);

            return HttpClientUtil.requestByGet(requestUrl);
        }

        public static string requestByGet(string url)
        {
            return HttpClientUtil.request(url, Method.GET);
        }

        public static string request(string url, Method methodType)
        {
            RestClient client = new RestClient(url);

            RestRequest request = new RestRequest(methodType);

            //request.AddHeader("postman-token", "b0486cf8-e6c8-b453-c7fa-32f5d37abc36");
            //request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);

            return client.Execute(request).Content;
        }

        public static string toQueryString(this Dictionary<string, string> source)
        {
            return String.Join("&", source.Select(kvp => String.Format("{0}={1}", HttpUtility.UrlEncode(kvp.Key), HttpUtility.UrlEncode(kvp.Value))).ToArray());
        }

        public static String makeUrl(String url, Dictionary<String, String> args)
        {
            return ((args == null) || (args.Count == 0)) ? url : url + "?" + HttpClientUtil.toQueryString(args);
        }
    }
}
