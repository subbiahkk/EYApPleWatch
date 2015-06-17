﻿using Cirrious.MvvmCross.ViewModels;
using EYAppleWatchPOC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EYAppleWatchPOC.Core.ViewModels
{
    public class EngTaskViewModel: MvxViewModel
    {

        public void Init(string eng)
        {
            Engagement = Newtonsoft.Json.JsonConvert.DeserializeObject<Engagement>(eng);
            EngTasks = GetEngagementTasks();
        }

        public EngTaskViewModel()
        {
            
           
        }

        public Engagement Engagement { get; set; }

        private List<EngTask> _EngTasks;

        public List<EngTask> EngTasks
        {
            get { return _EngTasks; }
            set { _EngTasks = value; RaisePropertyChanged(() => EngTasks); }
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

        private List<EngTask> GetEngagementTasks()
        {
            List<EngTask> engtasks = new List<EngTask>();
            for(int i = 1;i<5;i++)
            {
                EngTask engTask = new EngTask { Id = i, Description = "Task " + i.ToString(), EngagementId = Engagement.Id , EngDesc = Engagement.Description};
                engtasks.Add(engTask);
            }

            return engtasks;
            
        }

    }
}