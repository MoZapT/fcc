using Shared.Common;
using System;

namespace Shared.Models
{
    public class PersonBiography : BaseModel
    {
        public string PersonId { get; set; }
        public string BiographyText { get; set; }
    }
}