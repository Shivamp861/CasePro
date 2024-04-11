using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class YardLoadingRequestModel
    {
        public string? LiftingEquipmentUsed { get; set; }
        public string? ChainLiftingequipmenttobeused { get; set; }
        public string? IncidentReporting { get; set; }
        public string? AnyNearMissOccurrences { get; set; }
        public string? BarrierConditionChecks { get; set; }
        public string? AllRelevantActivityRams { get; set; }
        public string? Startandfinishtime { get; set; }
        
        public int ActivityId { get; set; }


    }
}
