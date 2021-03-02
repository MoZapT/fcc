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

        public Guid? MainPhotoId { get; set; }
        public PersonPhoto MainPhoto { get; set; }
        public ICollection<PersonPhoto> Photos { get; set; }
        public ICollection<PersonDocument> Files { get; set; }

        public ICollection<Relation> InviterRelations { get; set; }
        public ICollection<Relation> InvitedRelations { get; set; }
        public ICollection<PersonName> PreviousNames { get; set; }

        [NotMapped]
        public bool NameHasChanged { get; set; }
        [NotMapped]
        public bool IsMarried { get; set; }
        [NotMapped]
        public bool IsInPartnership { get; set; }
    }
}