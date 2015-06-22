
using Cirrious.MvvmCross.ViewModels;
using EYAppleWatchPOC.Core.Models;
using EYAppleWatchPOC.Core.Services;
using System.Collections.Generic;


namespace EYAppleWatchPOC.Core.ViewModels
{
    public class EngagementViewModel
        : MvxViewModel
    {
		public void Init()
		{
			
		}

		IEngagementService _engagementService;
		public EngagementViewModel(IEngagementService engagementService)
        {
			_engagementService = engagementService;

			RefreshData ();
            //Engagements = GetEngagements();
        }

        private List<Engagement> _Engagements;

        public List<Engagement> Engagements
        {
            get { return _Engagements; }
            set { _Engagements = value; RaisePropertyChanged(() => Engagements); }
        }


        private Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement> _ItemSelectedCommand;
        public System.Windows.Input.ICommand ItemSelectedCommand
        {
            get
            {
                _ItemSelectedCommand = _ItemSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<Engagement>(ShowEvidence);
                return _ItemSelectedCommand;
            }
        }

		private Cirrious.MvvmCross.ViewModels.MvxCommand _RefreshCommand;
		public System.Windows.Input.ICommand RefreshCommand
		{
			get
			{
				_RefreshCommand = _RefreshCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(RefreshData);
				return _RefreshCommand;
			}
		}
		private void RefreshData()
		{
			_engagementService.GetAllEngagements(result => Engagements = result,
				error => { });
		}
        private void ShowEvidence(Engagement engagement)
        {
            string strEngagement = Newtonsoft.Json.JsonConvert.SerializeObject(engagement);
            ShowViewModel<EngTaskViewModel>(new { eng = strEngagement });


        }

        private List<Engagement> GetEngagements()
        {
            List<Engagement> engs = new List<Engagement>();
            for(int i = 1;i<5;i++)
            {
                Engagement eng = new Engagement { Id = i, Description = "Engagement " + i.ToString()};
                engs.Add(eng);
            }

            return engs;
            
        }

    }
}
