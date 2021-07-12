using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OdinPlugs.OdinUtils.Utils.OdinHttp.Models
{
    public class RequestParamsModel
    {
        public string RequestQueryString { get; set; }
        public JObject RequestJson { get; set; }
        public string RequestFormDataString { get; set; }
        public JObject RequestFormData { get; set; }
        public List<Dictionary<string, UpLoadSmFile>> RequestUploadFile { get; set; }
    }
}