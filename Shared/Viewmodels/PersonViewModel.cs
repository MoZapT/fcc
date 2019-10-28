﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public PersonPartialViewRelationsModel RelationsPartialViewModel {get; set; }

        public Person Model { get; set; }
        public Person MarriedOn
        {
            get
            {
                var marriageRelation = RelationsPartialViewModel?.Relations?
                    .FirstOrDefault(e => e.RelationType == RelationType.HusbandWife);

                if (marriageRelation != null)
                    return marriageRelation.Invited;
                else
                    return null;
            }
        }
        public PersonBiography PersonBiography { get; set; }
        public List<Person> Models { get; set; }

        #endregion
    }
}