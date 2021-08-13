using System;
using Mapster;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinAdapterMapper
{
    public static class OdinTypeAdapterExtensions
    {
        /// <summary>
        /// 集合类型对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="T">最终映射转换后的类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static T OdinCollectionAdapter<TSource, TDestination, T>(this Object source)
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.NewConfig<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }
        /// <summary>
        /// 集合类型对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="options">自定义转换规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="T">最终映射转换后的类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static T OdinCollectionAdapter<TSource, TDestination, T>(
            this Object source,
            Action<TypeAdapterSetter<TSource, TDestination>> options
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.NewConfig<TSource, TDestination>();
            if (options != null)
                options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }
        /// <summary>
        /// 集合类型对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="config">全局映射规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="T">最终映射转换后的类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static T OdinCollectionAdapter<TSource, TDestination, T>(
            this Object source,
            TypeAdapterConfig config
            )
        {
            if (config == null)
                config = new TypeAdapterConfig();
            TypeAdapterSetter<TSource, TDestination> adapterSetter = config.ForType<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }

        /// <summary>
        /// 集合类型对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="options">自定义转换规则</param>
        /// <param name="config">全局映射规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="T">最终映射转换后的类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static T OdinCollectionAdapter<TSource, TDestination, T>(
            this Object source,
            Action<TypeAdapterSetter<TSource, TDestination>> options,
            TypeAdapterConfig config
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = config.ForType<TSource, TDestination>();
            options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }









        /// <summary>
        /// 对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static TDestination OdinAdapter<TSource, TDestination>(this Object source)
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.ForType<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }


        /// <summary>
        /// 对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="options">自定义转换规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static TDestination OdinAdapter<TSource, TDestination>(
            this TSource source,
            Action<TypeAdapterSetter<TSource, TDestination>> options
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.ForType<TSource, TDestination>();
            if (options != null)
                options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
        /// <summary>
        /// 对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="config">全局映射规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static TDestination OdinAdapter<TSource, TDestination>(
            this TSource source,
            TypeAdapterConfig config
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            if (config == null)
                config = new TypeAdapterConfig();
            adapterSetter = config.ForType<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
        /// <summary>
        /// 对象转换类型映射
        /// </summary>
        /// <param name="source">需要转换的源对象</param>
        /// <param name="options">自定义转换规则</param>
        /// <param name="config">全局映射规则</param>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <returns>通过映射规则转换后的对象, 具体使用请参看 readme</returns>
        public static TDestination OdinAdapter<TSource, TDestination>(
            this TSource source,
            Action<TypeAdapterSetter<TSource, TDestination>> options,
            TypeAdapterConfig config
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = config.ForType<TSource, TDestination>();
            options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
    }
}