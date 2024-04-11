using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class NoteRequestModel
    {
        public int Id { get; set; }

        public string? Notes { get; set; }

        public int? ActivityId { get; set; }

        public virtual ActivityTable? Activity { get; set; }
    }
}
