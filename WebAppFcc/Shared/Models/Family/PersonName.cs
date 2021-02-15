using WebAppFcc.Shared.Common;
using System;

namespace WebAppFcc.Shared.Models
{
    public class PersonName : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public string Name { get { return GetFullName(); } }
        public string PersonId { get; set; }
        public DateTime DateNameChanged { get; set; }

        private string GetFullName()
        {
            string result = string.Empty;
            result += string.IsNullOrWhiteSpace(Firstname) ? string.Empty : Firstname;
            result += string.IsNullOrWhiteSpace(result) ? string.Empty : " ";
            result += string.IsNullOrWhiteSpace(Lastname) ? string.Empty : Lastname;
            result += string.IsNullOrWhiteSpace(result) ? string.Empty : " ";
            result += string.IsNullOrWhiteSpace(Patronym) ? string.Empty : Patronym;
            return result;
        }
    }
}