using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CheckSubscription(Guid userId, string featureName)
        {
            var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            if (user == null)
                return false;

            var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == userId);
            if (subscription == null)
                return false;

            var subscriptionFeature = await Db.SubscriptionFeatures.FirstOrDefaultAsync(x => x.Name == featureName && x.SubscriptionId == subscription.SubscriptionId);
            if (subscriptionFeature == null)
                return false;

            if (string.IsNullOrEmpty(subscriptionFeature.Value))
                return false;

            int.TryParse(subscriptionFeature.ValueRemained, out var valueRemained);

            return valueRemained > 0;
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