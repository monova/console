using Microsoft.AspNetCore.Mvc;

namespace Monova.Web
{
    [Route("api/v1/[controller]")]
    public class ApiController : SecureDbController
    {
    }
}