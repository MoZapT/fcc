using Shared.Common;

namespace Shared.Models
{
    public class PersonBiography : BaseModel
    {
        public string PersonId { get; set; }
        public string BiographyText { get; set; }
    }
}