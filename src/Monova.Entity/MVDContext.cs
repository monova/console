using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Monova.Entity
{
    public class MVDContext : IdentityDbContext<MVDUser, IdentityRole<Guid>, Guid>
    {
        public MVDContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MVDMonitor> Monitors { get; set; }
        public DbSet<MVDMonitorStep> MonitorSteps { get; set; }
        public DbSet<MVDMonitorStepLog> MonitorStepLogs { get; set; }
        public DbSet<MVDSubscription> Subscriptions { get; set; }
        public DbSet<MVDSubscriptionFeature> SubscriptionFeatures { get; set; }
        public DbSet<MVDSubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<MVDSubscriptionTypeFeature> SubscriptionTypeFeatures { get; set; }
        public DbSet<MVDPayment> Payments { get; set; }
        public DbSet<MVDMonitorAlert> MonitorAlerts { get; set; }
        public DbSet<MVDMonitorAlertLog> MonitorAlertLogs { get; set; }
    }
}
