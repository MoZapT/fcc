using WebAppFcc.Shared.Common;
using System;

namespace WebAppFcc.Shared.Models
{
    public class Person : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public string Name { get { return GetFullName(); } }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool HasBirthDate { get { return BirthDate != null; } }
        public bool HasDeathDate { get { return DeathDate != null; } }
        public bool Sex { get; set; }
        public bool IsMarried { get; set; }
        public bool IsInPartnership { get; set; }
        public string FileContentId { get; set; }

        private string GetFullName()
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
}