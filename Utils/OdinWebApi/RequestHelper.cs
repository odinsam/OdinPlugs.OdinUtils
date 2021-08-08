using Newtonsoft.Json.Linq;

namespace OdinPlugs.OdinUtils.Utils.OdinWebApi
{
    /// <summary>
    /// web request helper class
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// GetRequestParams
        /// </summary>
        /// <param name="strParams">strParams</param>
        /// <param name="method">method</param>
        /// <returns></returns>
        public static JObject GetRequestParams(string strParams, string method)
        {

            JObject jobj = new JObject();
            if (method == "GET")
            {
                if (strParams.Length > 0)
                {
                    strParams = strParams.Substring(1);
                    var aryParams = strParams.Split('&');
                    if (aryParams.Length > 0)
                    {
                        foreach (var item in aryParams)
                        {
                            string key = item.Split('=')[0];
                            string val = item.Split('=')[1];
                            jobj.Add(key, val);
                        }
                        return jobj;
                    }
                    return null;
                }
                else
                    return null;
            }
            else
            {
                System.Console.WriteLine(strParams);
                return JObject.Parse(strParams);
            }
        }
    }
}