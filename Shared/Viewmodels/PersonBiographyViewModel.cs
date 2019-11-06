using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonBiographyViewModel : BaseViewModel
    {

        #region PROPERTIES

        public PersonBiography PersonBiography { get; set; }
        public Dictionary<ActivityType, List<PersonActivity>> Activities { get; set; }
        public List<ActivityType> ActivityTypeLoadingList { set; get; }

        #endregion

        public PersonBiographyViewModel()
        {
            InitActivitiesDictionaryList();
        }

        private void InitActivitiesDictionaryList()
        {
            Activities = new Dictionary<ActivityType, List<PersonActivity>>();
            ActivityTypeLoadingList = new List<ActivityType>();
            ActivityTypeLoadingList.Add(ActivityType.ElementarySchool);
            ActivityTypeLoadingList.Add(ActivityType.MiddleSchool);
            ActivityTypeLoadingList.Add(ActivityType.Highschool);
            ActivityTypeLoadingList.Add(ActivityType.Practice);
            ActivityTypeLoadingList.Add(ActivityType.College);
            ActivityTypeLoadingList.Add(ActivityType.TechnicalCollege);
            ActivityTypeLoadingList.Add(ActivityType.University);
            ActivityTypeLoadingList.Add(ActivityType.Working);
            ActivityTypeLoadingList.Add(ActivityType.Unemployed);
            ActivityTypeLoadingList.Add(ActivityType.Enterpreneur);
            ActivityTypeLoadingList.Add(ActivityType.Kindergarden);
            ActivityTypeLoadingList.Add(ActivityType.Other);
            ActivityTypeLoadingList.Add(ActivityType.Trainee);
        }
    }
}