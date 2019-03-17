using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Monova.Entity;

namespace Monova.Web
{
    public class MVBSMonitoring : IHostedService, IDisposable
    {
        public IServiceProvider Services { get; }
        private CancellationToken _token;

        public MVBSMonitoring(IServiceProvider services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _token = cancellationToken;
            DoWork();
            return Task.CompletedTask;
        }

        private async void DoWork()
        {
            while (true)
            {
                using (var scope = Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<MVDContext>();
                    var steps = await db.MonitorSteps
                                        .Where(x =>
                                            x.Type == MVDMonitorStepTypes.Request && x.Status != MVDMonitorStepStatusTypes.Processing &&
                                            DateTime.UtcNow > x.LastCheckDate.AddSeconds(x.Interval)
                                        )
                                        .OrderBy(x => x.LastCheckDate)
                                        .Take(20)
                                        .ToListAsync();

                    foreach (var step in steps)
                    {
                        var settings = step.SettingsAsRequest();
                        if (!string.IsNullOrEmpty(settings.Url))
                        {
                            var log = new MVDMonitorStepLog
                            {
                                MonitorId = step.MonitorId,
                                MonitorStepId = step.MonitorStepId,
                                StartDate = DateTime.UtcNow,
                                Interval = step.Interval,
                                Status = MVDMonitorStepStatusTypes.Processing
                            };
                            db.Add(log);
                            await db.SaveChangesAsync(_token);

                            try
                            {
                                var client = new HttpClient();
                                client.Timeout = TimeSpan.FromSeconds(15);
                                var result = await client.GetAsync(settings.Url, _token);
                                if (result.IsSuccessStatusCode)
                                {
                                    log.Status = MVDMonitorStepStatusTypes.Success;
                                }
                                else
                                {
                                    log.Status = MVDMonitorStepStatusTypes.Fail;
                                }
                            }
                            catch (HttpRequestException rex)
                            {
                                log.Log = rex.Message;
                                log.Status = MVDMonitorStepStatusTypes.Fail;
                            }
                            catch (Exception ex)
                            {
                                log.Log = ex.Message;
                                log.Status = MVDMonitorStepStatusTypes.Error;
                            }
                            finally
                            {
                                log.EndDate = DateTime.UtcNow;
                            }

                            if (log.Status == MVDMonitorStepStatusTypes.Success)
                                step.Status = MVDMonitorStepStatusTypes.Success;
                            else if (log.Status == MVDMonitorStepStatusTypes.Error)
                                step.Status = MVDMonitorStepStatusTypes.Error;
                            else
                                step.Status = MVDMonitorStepStatusTypes.Fail;
                        }
                        step.LastCheckDate = DateTime.UtcNow;
                        await db.SaveChangesAsync(_token);
                    }
                }
                await Task.Delay(500, _token);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // 
        }
    }
}