using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Mapster;
using Newtonsoft.Json;
using OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinString;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinObject
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 转换 对象 为 Dictionary&lt;string, string&gt;类型
        /// </summary>
        /// <typeparam name="string">key</typeparam>
        /// <typeparam name="string">value</typeparam>
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

        /// <summary>
        /// 对象转 JsonString
        /// </summary>
        /// <param name="obj">需要转换的对象</param>
        /// <param name="format">是否需要json格式化输出</param>
        /// <returns>转换后的string</returns>
        public static string ToJson(this Object obj, bool format = false)
        {
            if (!format)
                return JsonConvert.SerializeObject(obj);
            else
                return JsonConvert.SerializeObject(obj).ToJsonFormatString();
        }

        /// <summary>
        /// 可以处理复杂映射
        /// </summary>
        /// <typeparam name="TIn">输入类</typeparam>
        /// <typeparam name="TOut">输出类</typeparam>
        /// <param name="expression">表达式目录树,可以为null</param>
        /// <param name="tIn">输入实例</param>
        /// <returns></returns>
        public static TOut Mapper<TIn, TOut>(this TIn tIn, Expression<Func<TIn, TOut>> expression = null)
        {
            ParameterExpression parameterExpression = null;
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            parameterExpression = Expression.Parameter(typeof(TIn), "p");

            if (expression != null)
            {
                parameterExpression = expression.Parameters[0];
                if (expression.Body != null)
                {
                    memberBindingList.AddRange((expression.Body as MemberInitExpression).Bindings);
                }
            }
            foreach (var item in typeof(TOut).GetProperties())
            {
                if (typeof(TIn).GetProperty(item.Name) != null)
                {
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                if (typeof(TIn).GetField(item.Name) != null)
                {
                    MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            foreach (var item in typeof(TOut).GetFields())
            {
                if (typeof(TIn).GetField(item.Name) != null)
                {
                    MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                if (typeof(TIn).GetProperty(item.Name) != null)
                {
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());

            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
            {
                    parameterExpression
            });
            Func<TIn, TOut> func = lambda.Compile();//获取委托
            return func.Invoke(tIn);
        }
    }
}