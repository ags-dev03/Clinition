using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emergency.Classes

{
    public static class ApplicationConstant
    {
        public const string HisRegDomain = "http://hisreg.asmiglobalsoftwares.com/api/";
    }

    public class RegApiResponse
    {
        public int ResponseCode { get; set; }
        public bool ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponsePacket { get; set; }
    }
    public static class ResponseCode
    {
        public static int OK = 200;
        public static int Error = 201;
        public static int CallTokenError = 202;
        public static int AuthFail = 203;
    }
}