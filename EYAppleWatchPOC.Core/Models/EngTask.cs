using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYAppleWatchPOC.Core.Models
{
    public class EngTask
    {
        public int Id { get; set; }

        public string Description { get; set; }

		public int EngId { get; set; }

		public string Type { get; set; }

		public bool IsNew { get; set; }

     }
}
