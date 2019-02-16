using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monova.Entity;
using Newtonsoft.Json;

namespace Monova.Web.Controllers
{
    public class MonitoringController : ApiController
    {
        [NonAction]
        private async Task<object> GetMonitorClientModel(MVDMonitor monitor)
        {
            var url = string.Empty;

            var loadTime = 0.00;
            var loadTimes = new List<double>();

            var upTime = 0.00;
            var downTime = 0.00;
            var downTimePercent = 0.00;
            var totalMonitoredTime = 0;
            var upTimes = new List<double>();

            var monitorStepRequest = await Db.MonitorSteps.FirstOrDefaultAsync(x => x.MonitorId == monitor.MonitorId && x.Type == MVDMonitorStepTypes.Request);
            if (monitorStepRequest != null)
            {
                var requestSettings = monitorStepRequest.SettingsAsRequest();
                if (requestSettings != null)
                {
                    url = requestSettings.Url;
                }

                var week = DateTime.UtcNow.AddDays(-14);
                var logs = await Db.MonitorStepLogs
                                .Where(x => x.MonitorStepId == monitorStepRequest.MonitorStepId && x.StartDate >= week)
                                .OrderByDescending(x => x.StartDate)
                                .ToListAsync();

                if (logs.Any(x => x.Status == MVDMonitorStepStatusTypes.Success))
                {
                    loadTime = logs
                                .Where(x => x.Status == MVDMonitorStepStatusTypes.Success)
                                .Average(x => x.EndDate.Subtract(x.StartDate).TotalMilliseconds);
                }

                foreach (var log in logs)
                {
                    totalMonitoredTime += log.Interval;
                    if (log.Status == MVDMonitorStepStatusTypes.Success)
                        loadTimes.Add(log.EndDate.Subtract(log.StartDate).TotalMilliseconds);

                    if (log.Status == MVDMonitorStepStatusTypes.Fail)
                        downTime += log.Interval;
                }

                downTimePercent = 100 - (downTime / totalMonitoredTime) * 100;
            }

            return new
            {
                monitor.MonitorId,
                monitor.CreatedDate,
                monitor.LastCheckDate,
                monitor.MonitorStatus,
                monitor.Name,
                monitor.TestStatus,
                monitor.UpdatedDate,
                url,
                upTime,
                upTimes,
                downTime,
                downTimePercent,
                loadTime,
                loadTimes,
                totalMonitoredTime
            };
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute]Guid? id)
        {
            if (id.HasValue)
            {
                if (id.Value == Guid.Empty)
                {
                    return Error("You must send monitor id to get.");
                }

                var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == id.Value && x.UserId == UserId);
                if (monitor == null)
                    return Error("Monitor not found.", code: 404);

                return Success(data: await GetMonitorClientModel(monitor));
            }

            var list = await Db.Monitors.Where(x => x.UserId == UserId).ToListAsync();
            var clientList = new List<object>();

            foreach (var item in list)
                clientList.Add(await GetMonitorClientModel(item));

            return Success(null, list);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MVMMonitorSave value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                return Error("Name is required.");
            }

            var monitorCheck = await Db.Monitors.AnyAsync(
                x => x.MonitorId != value.Id &&
                x.Name.Equals(value.Name) &&
                x.UserId == UserId);

            if (monitorCheck)
            {
                return Error("This project name is already in use. Please choose a different name.");
            }

            MVDMonitor data = null;
            if (value.Id != Guid.Empty)
            {
                data = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == value.Id && x.UserId == UserId);
                if (data == null) return Error("Monitor not found.");

                data.UpdatedDate = DateTime.UtcNow;
                data.Name = value.Name;
            }
            else
            {
                data = new MVDMonitor
                {
                    MonitorId = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    Name = value.Name,
                    UserId = UserId
                };
                Db.Monitors.Add(data);
            }

            var monitorStepData = new MVDSMonitorStepSettingsRequest
            {
                Url = value.Url
            };

            var step = await Db.MonitorSteps.FirstOrDefaultAsync(x => x.MonitorId == data.MonitorId && x.Type == MVDMonitorStepTypes.Request);
            if (step != null)
            {
                var requestSettings = step.SettingsAsRequest() ?? new MVDSMonitorStepSettingsRequest();
                requestSettings.Url = value.Url;
                step.Settings = JsonConvert.SerializeObject(requestSettings);
            }
            else
            {
                step = new MVDMonitorStep
                {
                    MonitorStepId = Guid.NewGuid(),
                    Type = MVDMonitorStepTypes.Request,
                    MonitorId = data.MonitorId,
                    Settings = JsonConvert.SerializeObject(monitorStepData)
                };
                Db.MonitorSteps.Add(step);
            };

            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Monitoring saved successfully.", new
                {
                    Id = data.MonitorId
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