using Shared.Common;
using System;

namespace Shared.Models
{
    public class PersonBiography : BaseModel
    {
        public string BiographyText { get; set; }
        public string PersonId { get; set; }
    }
}