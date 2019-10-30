using Shared.Common;
using System;

namespace Shared.Models
{
    public class PersonActivity : BaseModel
    {
        public string BiographyId { get; set; }
        public string Activity { get; set; }
        public bool ActivityType { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }
}