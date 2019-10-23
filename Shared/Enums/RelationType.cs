using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum RelationType
    {
        [Description("Husband|Wife")]
        HusbandWife,
        [Description("Brother|Sister")]
        BrotherSister,

        //---- Relation types with counterparts!
        [Description("Father|Mother")]
        FatherMother,
        [Description("Son|Daughter")] //not translated yet!!!
        SonDaughter,
    }
}
