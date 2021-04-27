using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pinja.NetCore.WebApi.Util.OrderBy
{
    public class OrderByModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType.GetTypeInfo().IsGenericType &&
                context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(OrderByQueryString<>))
            {
                var types = context.Metadata.ModelType.GetGenericArguments();
                var o = typeof(OrderByModelBinder<>).MakeGenericType(types);

                return (IModelBinder)(Activator.CreateInstance(o)
                    ?? throw new InvalidOperationException($"Could not create instance of type {o}"));
            }

            return null;
        }
    }
}
