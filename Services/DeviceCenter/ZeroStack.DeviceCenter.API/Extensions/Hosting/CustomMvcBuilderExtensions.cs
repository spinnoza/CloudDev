using FluentValidation.AspNetCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomExtensions(this IMvcBuilder builder)
        {
            builder
                //添加FluentValidation模型验证
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                //添加模型验证本地化
                .AddDataAnnotationsLocalization();
            return builder;
        }
    }
}
