using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class SignoffRequestModel
    {
        public DateTime CompetionDate { get; set; }

        public string PrintName { get; set; }

        public DateTime SignOffDate { get; set; }

        public string Signature { get; set; }

        public int ActivityId { get; set; } 
    }
}
