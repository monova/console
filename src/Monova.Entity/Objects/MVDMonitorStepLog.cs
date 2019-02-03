using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monova.Entity
{
    [Table("MonitorStepLog")]
    public class MVDMonitorStepLog
    {
        [Key]
        public Guid MonitorStepLogId { get; set; }
        public Guid MonitorStepId { get; set; }
        public Guid MonitorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MVDMonitorStepStatusTypes Status { get; set; }
        public string Log { get; set; }
    }

    public enum MVDMonitorStepStatusTypes : short
    {
        Fail = 0,
        Success = 1
    }
}