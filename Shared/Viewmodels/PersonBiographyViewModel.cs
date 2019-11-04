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

        public List<PersonActivity> Kindergarden { get; set; }
        public List<PersonActivity> ElementarySchool { get; set; }
        public List<PersonActivity> MiddleSchool { get; set; }
        public List<PersonActivity> Highschool { get; set; }
        public List<PersonActivity> Practice { get; set; }
        public List<PersonActivity> College { get; set; }
        public List<PersonActivity> TechnicalCollege { get; set; }
        public List<PersonActivity> Trainee { get; set; }
        public List<PersonActivity> University { get; set; }
        public List<PersonActivity> Unemployed { get; set; }
        public List<PersonActivity> Working { get; set; }
        public List<PersonActivity> Enterpreneur { get; set; }
        public List<PersonActivity> Other { get; set; }

        #endregion

        public PersonBiographyViewModel()
        {
            Kindergarden = new List<PersonActivity>();
            ElementarySchool = new List<PersonActivity>();
            MiddleSchool = new List<PersonActivity>();
            Highschool = new List<PersonActivity>();
            Practice = new List<PersonActivity>();
            College = new List<PersonActivity>();
            TechnicalCollege = new List<PersonActivity>();
            Trainee = new List<PersonActivity>();
            University = new List<PersonActivity>();
            Unemployed = new List<PersonActivity>();
            Working = new List<PersonActivity>();
            Enterpreneur = new List<PersonActivity>();
            Other = new List<PersonActivity>();
        }
    }
}