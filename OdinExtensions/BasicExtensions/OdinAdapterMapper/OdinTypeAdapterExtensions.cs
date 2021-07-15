using System;
using System.Linq.Expressions;
using Mapster;
using OdinPlugs.OdinUtils.Utils.OdinAdapterMapper;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinAdapterMapper
{
    public static class OdinTypeAdapterExtensions
    {
        public static T OdinTypeAdapterBuilder<TSource, TDestination, T>(
            this Object source,
            Action<TypeAdapterSetter<TSource, TDestination>> options = null
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.NewConfig<TSource, TDestination>();
            if (options != null)
                options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }
        public static T OdinTypeAdapterBuilder<TSource, TDestination, T>(
            this Object source,
            TypeAdapterConfig config = null
            )
        {
            if (config == null)
                config = new TypeAdapterConfig();
            TypeAdapterSetter<TSource, TDestination> adapterSetter = config.ForType<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }

        public static T OdinTypeAdapterBuilder<TSource, TDestination, T>(
            this Object source,
            Action<TypeAdapterSetter<TSource, TDestination>> options,
            TypeAdapterConfig config
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = config.ForType<TSource, TDestination>();
            options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<T>(); ;
        }


        public static TDestination OdinTypeAdapterBuilder<TSource, TDestination>(
            this TSource source,
            Action<TypeAdapterSetter<TSource, TDestination>> options = null
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            var config = new TypeAdapterConfig();
            adapterSetter = config.ForType<TSource, TDestination>();
            if (options != null)
                options(adapterSetter);
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
        public static TDestination OdinTypeAdapterBuilder<TSource, TDestination>(
            this TSource source,
            TypeAdapterConfig config = null
            )
        {
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            if (config == null)
                config = new TypeAdapterConfig();
            adapterSetter = config.ForType<TSource, TDestination>();
            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
        public static TDestination OdinTypeAdapterBuilder<TSource, TDestination>(
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