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
        [Description("Никто")]
        NotRelated,
        [Description("Брат")]
        Brother,
        [Description("Сестра")]
        Sister,
        [Description("Отец")]
        Father,
        [Description("Мать")]
        Mother,
        [Description("Тесть")]
        FatherInLaw,
        [Description("Тёща")]
        MotherInLaw,
        [Description("Брат(?)")]
        BrotherInLaw,
        [Description("Сестра(?)")]
        SisterInLaw,
        [Description("Муж")]
        Husband,
        [Description("Жена")]
        Wife,
    }
}
