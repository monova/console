using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Monova.Web
{
    [Authorize]
    public class SecureController : Controller
    {
        
    }
}