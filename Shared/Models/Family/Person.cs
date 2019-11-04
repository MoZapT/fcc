using Shared.Common;
using System;

namespace Shared.Models
{
    public class Person : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public string Name { get { return GetFullName(); } }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool HasBirthDate { get { return (BirthDate == null ? false : true); } }
        public bool HasDeathDate { get { return (DeathDate == null ? false : true); } }
        public bool Sex { get; set; }
        public bool IsMarried { get; set; }
        public bool IsInPartnership { get; set; }
        public string FileContentId { get; set; }

        private string GetFullName()
        {
            string result = "";
            result += string.IsNullOrWhiteSpace(Firstname) ? "" : Firstname;
            result += string.IsNullOrWhiteSpace(result) ? "" : " ";
            result += string.IsNullOrWhiteSpace(Lastname) ? "" : Lastname;
            result += string.IsNullOrWhiteSpace(result) ? "" : " ";
            result += string.IsNullOrWhiteSpace(Patronym) ? "" : Patronym;
            return result;
        }
    }
}