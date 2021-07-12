using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinString;

namespace OdinPlugs.OdinUtils.Utils.OdinObject.OdinObject
{
    public static class OdinObjectHelper
    {
        public static Dictionary<string, string> ConvertObjectToDictionary<T>(this T obj, Encoding encoder = null) where T : class
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (typeof(T) == typeof(string))
            {
                foreach (var item in obj.ToString().Split('&'))
                {
                    dic.Add(
                        item.Split('=')[0],
                        encoder == null || encoder == Encoding.UTF8 ?
                        item.Split('=')[1]
                        :
                        item.Split('=')[1].ConvertStringEncode(Encoding.UTF8, encoder)
                        );
                }
            }
            else
            {
                foreach (var item in obj.GetType().GetRuntimeProperties())
                {
                    dic.Add(item.Name,
                            encoder == null || encoder == Encoding.UTF8 ?
                            item.GetValue(obj).ToString()
                            :
                            item.GetValue(obj).ToString().ConvertStringEncode(Encoding.UTF8, encoder)
                            );
                }
            }
            return dic;
        }
    }
}