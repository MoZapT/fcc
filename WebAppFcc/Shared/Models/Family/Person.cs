using WebAppFcc.Shared.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebAppFcc.Shared.Models
{
    public class Person : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public string Name { get 
            {
                string result = "";
                result += string.IsNullOrWhiteSpace(Firstname) ? string.Empty : Firstname;
                result += string.IsNullOrWhiteSpace(result) ? string.Empty : " ";
                result += string.IsNullOrWhiteSpace(Lastname) ? string.Empty : Lastname;
                result += string.IsNullOrWhiteSpace(result) ? string.Empty : " ";
                result += string.IsNullOrWhiteSpace(Patronym) ? string.Empty : Patronym;
                return result;
            } 
        }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool HasBirthDate { get { return BirthDate != null; } }
        public bool HasDeathDate { get { return DeathDate != null; } }
        public bool Sex { get; set; }
        public Guid? FileContentId { get; set; }
        public IEnumerable<Relation> Relations { get; set; }

        [NotMapped]
        public string Base64Icon { get; set; }
        [NotMapped]
        public bool IsMarried { get; set; }
        [NotMapped]
        public bool IsInPartnership { get; set; }
    }
}