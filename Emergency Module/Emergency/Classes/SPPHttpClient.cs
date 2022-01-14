using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Emergency.Classes
{
    public class SPPHttpClient
    {
        public string GetHttpRequest(string apiurl)
        {
            string response;
            WebRequest tRequest = WebRequest.Create(apiurl);
            tRequest.Method = "get";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add("AuthPswd", "7D29DB6A3B39DC6A209B7FD2935F8A40");
            tRequest.Headers.Add("CompName", "0E2F5C95742FC7A2CCED70C79A19DCE0");
            using (WebResponse tResponse = tRequest.GetResponse())
            {
                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                {
                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                    {
                         response = tReader.ReadToEnd();
                    }
                }
            }
            return response;
        }


        public string PostHttpRequest(string apiurl,object data)
        {
            string response;
            WebRequest tRequest = WebRequest.Create(apiurl);
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add("AuthPswd", "7D29DB6A3B39DC6A209B7FD2935F8A40");
            tRequest.Headers.Add("CompName", "0E2F5C95742FC7A2CCED70C79A19DCE0");
            Byte[] byteArray=null;
            if (data != null)
            {
                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(data);
                byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.ContentLength = byteArray.Length;
            }
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                if(byteArray != null && byteArray.Length>0)
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                          response = tReader.ReadToEnd();
                        }
                    }
                }
            }
            return response;
        }










    }
}