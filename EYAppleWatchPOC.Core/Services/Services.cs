using EYAppleWatchPOC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYAppleWatchPOC.Core.Services
{
    public interface IEngagementService
    {
        void GetAllEngagements(Action<List<Engagement>> Success, Action<Exception> Error);
    }

    public interface ITaskService
    {
        void GetAllTasks(string engagementId, Action<List<EngTask>> Success, Action<Exception> Error);
    }
}
