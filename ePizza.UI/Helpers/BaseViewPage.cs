using ePizza.UI.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace ePizza.UI.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {

        [RazorInject]
        public IUserAccessor userAccessor { get; set; }

    }
}
