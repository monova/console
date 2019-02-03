using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monova.Entity
{
    [Table("Monitor")]
    public class MVDMonitor
    {
        [Key]
        public Guid MonitorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public MVDMonitorStatusTypes MonitorStatus { get; set; }
        public MVDTestStatusTypes TestStatus { get; set; }
        public DateTime LastCheckDate { get; set; }
        public decimal UpTime { get; set; }
        public int LoadTime { get; set; }
        public short MonitorTime { get; set; }
    }

    public enum MVDMonitorStatusTypes : short
    {
        Down = 0,
        Up = 1,
        Warning = 2
    }

    public enum MVDTestStatusTypes : short
    {
        Fail = 0,
        AllPassed = 1,
        Warning = 2,
    }
}