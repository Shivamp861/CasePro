using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class InstructOperation
    {
        public string SelectedActivity { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public string Note { get; set; }

        public int ActivityId { get; set; }

        public List<InstructorName> InstructorNames { get; set; }
    }
    
}
