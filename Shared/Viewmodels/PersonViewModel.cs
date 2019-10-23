using System.Collections.Generic;
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
        public bool IsMarried {
            get
            {
                var marriageRelation = RelationsPartialViewModel?.Relations?
                    .FirstOrDefault(e => e.RelationType == RelationType.HusbandWife);

                return marriageRelation == null ? false : true;
            }
        }
        public Person MarriedOn
        {
            get
            {
                var marriageRelation = RelationsPartialViewModel?.Relations?
                    .FirstOrDefault(e => e.RelationType == RelationType.HusbandWife);

                return marriageRelation.Invited;
            }
        }
        public List<Person> Models { get; set; }

        #endregion
    }
}