using ePizza.Entities.Concrete;
using ePizza.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Controllers
{
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
