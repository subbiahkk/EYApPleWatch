
using Cirrious.MvvmCross.ViewModels;
using EYAppleWatchPOC.Core.Models;
using EYAppleWatchPOC.Core.Services;
using System.Collections.Generic;


namespace EYAppleWatchPOC.Core.ViewModels
{
    public class EngagementViewModel
        : MvxViewModel
    {

        public EngagementViewModel()
        {
            //engagementService.GetAllEngagements(result => Engagements = result,
            //    error => { });

            Engagements = GetEngagements();
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

        private void ShowEvidence(Engagement engagement)
        {
            string strEngagement = Newtonsoft.Json.JsonConvert.SerializeObject(engagement);
            //ShowViewModel<EvidenceViewModel>(new { eng = strEngagement });


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
