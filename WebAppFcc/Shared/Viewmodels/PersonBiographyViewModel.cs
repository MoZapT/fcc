using System.Collections.Generic;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
{
    public class PersonBiographyViewModel : BaseViewModel
    {

        #region PROPERTIES

        public PersonBiography PersonBiography { get; set; }
        public Dictionary<ActivityType, IEnumerable<PersonActivity>> Activities { get; set; }
        public IEnumerable<ActivityType> ActivityTypeLoadingList { set; get; }

        #endregion

        public PersonBiographyViewModel()
        {
            InitActivitiesDictionaryList();
        }

        private void InitActivitiesDictionaryList()
        {
            Activities = new Dictionary<ActivityType, IEnumerable<PersonActivity>>();
            var activities = new List<ActivityType>();
            activities.Add(ActivityType.ElementarySchool);
            activities.Add(ActivityType.MiddleSchool);
            activities.Add(ActivityType.Highschool);
            activities.Add(ActivityType.Practice);
            activities.Add(ActivityType.College);
            activities.Add(ActivityType.TechnicalCollege);
            activities.Add(ActivityType.University);
            activities.Add(ActivityType.Working);
            activities.Add(ActivityType.Unemployed);
            activities.Add(ActivityType.Enterpreneur);
            activities.Add(ActivityType.Kindergarden);
            activities.Add(ActivityType.Other);
            activities.Add(ActivityType.Trainee);
            ActivityTypeLoadingList = activities;
        }
    }
}