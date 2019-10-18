using Shared.Common;
using System;

namespace Shared.Models
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