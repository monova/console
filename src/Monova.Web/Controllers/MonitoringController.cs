using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monova.Entity;

namespace Monova.Web.Controllers
{
    public class MonitoringController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await Db.Monitors.ToListAsync();
            return Success(null, list);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MVDMonitor value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                return Error("Name is required.");
            }
            var dataObject = new MVDMonitor
            {
                CreatedDate = DateTime.UtcNow,
                Name = value.Name
            };
            Db.Monitors.Add(dataObject);
            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Monitoring saved successfully.", new
                {
                    Id = dataObject.MonitorId
                });
            else
                return Error("Something is wrong with your model.");
        }
    }

    public class MonitoringModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}