using Shared.Enums;
using System;
using System.Collections.Generic;

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

        public static IEnumerable<RelationType> GetRelationTypeExclusion(RelationType type)
        {
            List<RelationType> list = new List<RelationType>();

            switch (type)
            {
                case RelationType.BrotherSister:
                case RelationType.SonDaughter:
                case RelationType.FatherMother:
                    list.Add(RelationType.BrotherSister);
                    list.Add(RelationType.SonDaughter);
                    list.Add(RelationType.FatherMother);

                    return list;
                default:
                    return list;
            }
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> GetFamilySiblingsSelectGroup(bool isFemale = false)
        {
            var rMgr = Resources.Resource.ResourceManager;

            return new List<System.Web.Mvc.SelectListItem>()
            {
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.FatherMother).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.FatherMother, isFemale)
                    },
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.SonDaughter).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.SonDaughter, isFemale)
                    },
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.BrotherSister).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.BrotherSister, isFemale)
                    },
            };
        }
    }
}