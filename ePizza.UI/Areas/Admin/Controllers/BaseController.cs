using ePizza.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
