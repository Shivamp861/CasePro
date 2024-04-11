using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ResponseModel
{
	public class LoginResponseModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }

        public DateTime LastLogindate { get; set; }
    }
}
