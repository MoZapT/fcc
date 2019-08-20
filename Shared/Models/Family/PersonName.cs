using Shared.Common;
using System;

namespace Shared.Models
{
    public class PersonName : BaseEntity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public string PersonId { get; set; }
    }
}