using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Text;
using System.Data;
using RestSharp;
using System.Net;

namespace PocWinForm.Agents
{

    public class APIAgents
    {

        public static HttpStatusCode HttpPost(string inUrl, Dictionary<string, string> inHeader, Dictionary<string, string> inValue, ref string ioContent)
        {
            ioContent = "";
            var client = new RestClient(inUrl);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("cache-control", "no-cache");

            foreach (var header in inHeader)
            {
                request.AddHeader(header.Key, header.Value);
            };

            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("application/json", json, ParameterType.RequestBody);

            request.AddJsonBody(inValue); // uses JsonSerializer
            var response = client.Execute(request);
            ioContent = response.Content;
            return response.StatusCode;
        }

        public static HttpStatusCode HttpPostRaw(string inUrl, Dictionary<string, string> inHeader, string inValue, ref string ioContent)
        {
            ioContent = "";
            var client = new RestClient(inUrl);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("cache-control", "no-cache");

            foreach (var header in inHeader)
            {
                request.AddHeader(header.Key, header.Value);
            };

            request.RequestFormat = DataFormat.Json;

            request.AddParameter("application/json", inValue, ParameterType.RequestBody);

            //request.AddJsonBody(inValue); // uses JsonSerializer

            var response = client.Execute(request);
            ioContent = response.Content;
            return response.StatusCode;
        }

        public static HttpStatusCode HttpGet(string inUrl, Dictionary<string, string> inHeader, ref string ioContent)
        {
            ioContent = "";
            var client = new RestClient(inUrl);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("cache-control", "no-cache");
            foreach (var header in inHeader)
            {
                request.AddHeader(header.Key, header.Value);
            };

            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            ioContent = response.Content;
            return response.StatusCode;
        }


        public static string GenSessionCode(string inChannel, DateTime inDate)
        {

            string strSessionCode = inChannel + APIAgents.RandomDigits(9) + inDate.ToString("yyyyMMddHHmmss");
            return strSessionCode;
        }

        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }


    }

}
