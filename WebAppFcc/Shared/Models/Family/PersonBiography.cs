using WebAppFcc.Shared.Common;

namespace WebAppFcc.Shared.Models
{
    public class PersonBiography : BaseModel
    {
        public string PersonId { get; set; }
        public string BiographyText { get; set; }
    }
}