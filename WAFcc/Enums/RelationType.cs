using System.ComponentModel;

namespace WAFcc.Enums
{
    public enum RelationType
    {
        [Description("Husband|Wife")]
        HusbandWife,
        [Description("Brother|Sister")]
        BrotherSister,

        [Description("Father|Mother")]
        FatherMother,
        [Description("Son|Daughter")]
        SonDaughter,

        [Description("LivePartnerMale|LivePartnerFemale")]
        LivePartner,

        [Description("InLawFather|InLawMother")]
        InLawFatherMother,
        [Description("InLawSon|InLawDaughter")]
        InLawSonDaughter,
        [Description("InLawBrother|InLawSister")]
        InLawBrotherSister,

        [Description("StepBrother|StepSister")]
        StepBrotherSister,
        [Description("StepFather|StepMother")]
        StepFatherMother,
        [Description("StepSon|StepDaughter")]
        StepSonDaughter,
        //[Description("FosterSon|FosterDaughter")]
        //FosterSonDaughter,

        [Description("Uncle|Aunt")]
        UncleAunt,
        [Description("CousinMale|CousinFemale")]
        Cousins,
        [Description("Nephew|Niece")]
        NephewNiece,

        [Description("Grandson|Granddaughter")]
        GrandSonDaughter,
        [Description("Grandfather|Grandmother")]
        GrandFatherMother,

        [Description("SiblingMale|SiblingFemale")]
        Siblings,
        [Description("InLawSiblingMale|InLawSiblingFemale")]
        InLawSiblings,
    }
}
