using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class ResourseDetails
    {
        public int Id { get; set; }

        public string Shift { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string ResourceType { get; set; }
        public string Comments { get; set; }

        public string DayNight { get; set; }
        public int ActivityId { get; set; }

    }
}
