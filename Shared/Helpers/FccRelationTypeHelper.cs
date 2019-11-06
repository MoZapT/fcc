using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Helpers
{
    public static class FccRelationTypeHelper
    {
        public static RelationType GetCounterRelationType(RelationType type)
        {
            switch (type)
            {
                case RelationType.FatherMother:
                    return RelationType.SonDaughter;
                case RelationType.SonDaughter:
                    return RelationType.FatherMother;
                default:
                    return type;
            }
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> GetFamilySiblingsSelectGroup(bool isFemale = false)
        {
            return FccEnumHelper.GetTranslatedSelectListItemCollection<RelationType>
                (typeof(RelationType), isFemale)
                .Where(e => e.Value != RelationType.HusbandWife.ToString())
                .Where(e => e.Value != RelationType.LivePartner.ToString());
        }
    }
}