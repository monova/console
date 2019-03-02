using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Monova.Entity;

namespace Monova.Web
{
    [Route("api/v1/[controller]")]
    public class ApiController : SecureDbController
    {
        private UserManager<MVDUser> _userManager;
        public UserManager<MVDUser> UserManager => _userManager ?? (UserManager<MVDUser>)HttpContext?.RequestServices.GetService(typeof(UserManager<MVDUser>));

        public Guid UserId
        {
            get
            {
                var userId = UserManager.GetUserId(User);
                return Guid.Parse(userId);
            }
        }

        [NonAction]
        public IActionResult Success(string message = default(string), object data = default(object), int code = 200)
        {
            return Json(
                new MVReturn()
                {
                    Success = true,
                    Message = message,
                    Data = data,
                    Code = code
                }
            );
        }

        [NonAction]
        public IActionResult Error(string message = default(string), string internalMessage = default(string), object data = default(object), int code = 400, List<MVReturnError> errorMessages = null)
        {
            var rv = new MVReturn()
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data,
                Code = code
            };

            if (rv.Code == 500)
                return StatusCode(500, rv);
            if (rv.Code == 401)
                return Unauthorized();
            if (rv.Code == 403)
                return Forbid();
            if (rv.Code == 404)
                return NotFound(rv);

            return BadRequest(rv);
        }
    }
}