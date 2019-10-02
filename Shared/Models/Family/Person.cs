using Shared.Common;
using System;

namespace Shared.Models
{
    public class Person : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public bool HasBirthDate { get; set; }
        public bool HasDeathDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool Sex { get; set; }

        public Person()
        {
            if (BirthDate != null)
            {
                HasBirthDate = true;
            }

            if (DeathDate != null)
            {
                HasDeathDate = true;
            }
        }

        public string GetFullName()
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