using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AssistantApp
{
    class UtilRequest
    {
        public string JSONstring { get; set; }
        public UtilRequest()
        {
        }


        public async Task<string> GetContentFromUrl(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(sURL));
            request.Accept = "application/json";
            request.Method = "GET";
            StringBuilder output = null;
            try
            {
                WebResponse response = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request); //request.BeginGetResponse(GetJSONCallback, request);
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                output = new StringBuilder();
                output.Append(sr.ReadToEnd());
            }
            catch(WebException exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
            }
            JSONstring = output.ToString();
            System.Diagnostics.Debug.WriteLine(JSONstring);
            return output.ToString();
        }
    }
}
