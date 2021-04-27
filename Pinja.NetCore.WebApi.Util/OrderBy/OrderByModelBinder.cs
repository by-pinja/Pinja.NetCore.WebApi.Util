using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pinja.NetCore.WebApi.Util.OrderBy
{
    // Using modelbinder+provider instead of typeconverter because generic type is needed.
    public class OrderByModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(new OrderByQueryString<T>());
                return Task.CompletedTask;
            }

            var ssp = new OrderByQueryString<T>(valueProviderResult.First());

            bindingContext.Result = ModelBindingResult.Success(ssp);

            return Task.CompletedTask;
        }
    }
}
