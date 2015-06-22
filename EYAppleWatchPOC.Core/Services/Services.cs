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
		void AddTask(string engId,string toDoTask, Action<bool> Success, Action<Exception> Error);
    }



	public class EngagementService : IEngagementService
	{
		private readonly IRestService _SimpleRestService;        

		public EngagementService(IRestService simpleRestService)
		{
			_SimpleRestService = simpleRestService;
		}

		public void GetAllEngagements(Action<List<Engagement>> success, Action<Exception> error)
		{
			_SimpleRestService.MakeRequest<List<Engagement>>(
				"http://eyapplewatch.azurewebsites.net/api/engagement", "Get", success, error);              
		}
	}
}
