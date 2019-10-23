using Shared.Enums;
using System;

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
    }
}