using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYAppleWatchPOC.Core.Models
{
    public class Engagement
    {
        public Engagement()
        {
            //ClientName = "Coca Cola";
        }

        public int Id { get; set; }

        public string Description { get; set; }
       

        public string ClientName { get; set; }

		public List<EngTask> Tasks {
			get;
			set;
		}
    }
}
