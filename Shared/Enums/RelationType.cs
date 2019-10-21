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
        [Description("Brother|Sister")]
        BrotherSister,
        [Description("Father|Mother")]
        FatherMother,
        //[Description("FatherInLaw|MotherInLaw")]
        //FatherMotherInLaw,
        //[Description("Husband|Wife")]
        //HusbandWife,
    }
}
