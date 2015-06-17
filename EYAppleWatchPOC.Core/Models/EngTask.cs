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

        public int EngagementId { get; set; }

        public string EngDesc { get; set; }
    }
}
