using System;
using System.Linq.Expressions;
using Mapster;
using OdinPlugs.OdinUtils.Utils.OdinAdapterMapper;

namespace OdinPlugs.OdinUtils.OdinExtensions.BasicExtensions.OdinAdapterMapper
{
    public static class OdinTypeAdapterExtensions
    {
        public static TypeAdapterBuilder<Object> OdinTypeAdapterBuilder<TSource, TDestination>(
            this Object source, Action<TypeAdapterSetter<TSource, TDestination>> options, bool IsEnhanceConfig = true
            )
        {
            var config = new TypeAdapterConfig();
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            if (!IsEnhanceConfig)
                adapterSetter = config.NewConfig<TSource, TDestination>();
            else
                adapterSetter = config.ForType<TSource, TDestination>();

            options(adapterSetter);

            return source.BuildAdapter(config);
        }



        public static TDestination OdinTypeAdapterBuilder<TSource, TDestination>(
            this TSource source, Action<TypeAdapterSetter<TSource, TDestination>> options, bool IsEnhanceConfig = true
            )
        {
            var config = new TypeAdapterConfig();
            TypeAdapterSetter<TSource, TDestination> adapterSetter = null;
            if (!IsEnhanceConfig)
                adapterSetter = config.NewConfig<TSource, TDestination>();
            else
                adapterSetter = config.ForType<TSource, TDestination>();

            options(adapterSetter);

            return source.BuildAdapter(config).AdaptToType<TDestination>();
        }
    }
}