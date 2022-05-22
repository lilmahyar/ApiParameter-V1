using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;

namespace ApiParameter_V1.ModelBinders
{
    public class ApiParametersModelBinder : IModelBinder
    {
        private static readonly IEnumerable<string> RouteDataValueParameters = (IEnumerable<string>)new string[2]
        {
            "action",
            "controller"
        };
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpRequest request;
            ApiParametersModelBinder parameters;
            IQueryCollection quertCollection;
            MediaTypeHeaderValue contentType;
            RouteValueDictionary routeValues;
            if(bindingContext.ModelType != typeof(Apiparameters))
        }
    }
}
