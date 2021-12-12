using WAFcc.Enums;

namespace WAFcc.Models
{
    public class PersonActivity : BaseModel
    {
        public string BiographyId { get; set; }
        public string Activity { get; set; }
        public ActivityType ActivityType { get; set; }
        public bool HasBegun { get; set; }
        public DateTime? DateBegin { get; set; }
        public bool HasEnded { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}