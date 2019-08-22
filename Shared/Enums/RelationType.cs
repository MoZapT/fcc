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
        [Description("Брат||Сестра")]
        BrotherSister,
        [Description("Отец||Мать")]
        FatherMother,
        [Description("Тесть||Тёща")]
        FatherInLawMotherInLaw,
        [Description("Брат(?)||Сестра(?)")]
        BrotherInLawSisterInLaw,
        [Description("Муж||Жена")]
        HusbandWife,
        [Description("Сын||Дочь")]
        SonDaughter,
    }
}
