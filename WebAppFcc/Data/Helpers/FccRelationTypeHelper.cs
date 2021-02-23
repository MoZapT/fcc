using WebAppFcc.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

namespace WebAppFcc.Shared.Helpers
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
                case RelationType.StepFatherMother:
                    return RelationType.StepSonDaughter;
                case RelationType.StepSonDaughter:
                    return RelationType.StepFatherMother;
                case RelationType.GrandFatherMother:
                    return RelationType.GrandSonDaughter;
                case RelationType.GrandSonDaughter:
                    return RelationType.GrandFatherMother;
                case RelationType.UncleAunt:
                    return RelationType.NephewNiece;
                case RelationType.NephewNiece:
                    return RelationType.UncleAunt;
                case RelationType.InLawFatherMother:
                    return RelationType.InLawSonDaughter;
                case RelationType.InLawSonDaughter:
                    return RelationType.InLawFatherMother;
                default:
                    return type;
            }
        }

        //public static IEnumerable<System.Web.Mvc.SelectListItem> GetFamilySiblingsSelectGroup(bool isFemale = false)
        //{
        //    return FccEnumHelper.GetTranslatedSelectListItemCollection<RelationType>
        //        (typeof(RelationType), isFemale)
        //        .Where(e => e.Value != RelationType.HusbandWife.ToString())
        //        .Where(e => e.Value != RelationType.LivePartner.ToString());
        //}
    }
}