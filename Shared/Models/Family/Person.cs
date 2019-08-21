using Shared.Common;
using System;

namespace Shared.Models
{
    public class Person : BaseModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public bool BornTimeKnown { get; set; }
        public bool DeadTimeKnown { get; set; }
        public DateTime? BornTime { get; set; }
        public DateTime? DeadTime { get; set; }
        public bool Sex { get; set; }
    }
}