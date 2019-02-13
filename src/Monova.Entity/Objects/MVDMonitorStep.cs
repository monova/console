using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Monova.Entity
{
    [Table("MonitorStep")]
    public class MVDMonitorStep
    {
        [Key]
        public Guid MonitorStepId { get; set; }
        public Guid MonitorId { get; set; }
        public MVDMonitorStepTypes Type { get; set; }
        public string Settings { get; set; }

        public MVDSMonitorStepSettingsRequest SettingsAsRequest()
        {
            return JsonConvert.DeserializeObject<MVDSMonitorStepSettingsRequest>(Settings);
        }
    }

    public enum MVDMonitorStepTypes : short
    {
        Request = 1,
        StatusCode = 2,
        HeaderExists = 3,
        BodyContains = 4,
    }

    public class MVDSMonitorStepSettingsRequest
    {
        public string Url { get; set; }
    }
}