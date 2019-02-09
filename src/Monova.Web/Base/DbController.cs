using Microsoft.AspNetCore.Mvc;
using Monova.Entity;

namespace Monova.Web
{
    public class DbController : Controller
    {
        private MVDContext _db;
        public MVDContext Db => _db ?? (MVDContext)HttpContext?.RequestServices.GetService(typeof(MVDContext));
    }
}