using ePizza.Entities.Concrete;
using ePizza.UI.Helpers;
using ePizza.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Areas.Users.Controllers
{

    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        IUserAccessor _userAccessor;

        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public User CurrentUser
        {
            get
            {
                if (User!=null)
                {
                    return _userAccessor.GetUser();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
