using System.ComponentModel;

namespace WebAppFcc.Shared.Enums
{
    public enum ActivityType
    {
        [Description("ElementarySchool")]
        ElementarySchool,
        [Description("MiddleSchool")]
        MiddleSchool,
        [Description("Highschool")]
        Highschool,
        [Description("Practice")]
        Practice,
        [Description("College")]
        College,
        [Description("TechnicalCollege")]
        TechnicalCollege,
        [Description("University")]
        University,
        [Description("Working")]
        Working,
        [Description("Unemployed")]
        Unemployed,
        [Description("Enterpreneur")]
        Enterpreneur,
        [Description("Kindergarden")]
        Kindergarden,
        [Description("Other")]
        Other,
        [Description("Trainee")]
        Trainee,
    }
}
