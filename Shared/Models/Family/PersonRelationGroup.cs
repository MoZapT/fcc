using Shared.Common;
using Shared.Enums;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public class PersonRelationGroup : BaseModel
    {
        public List<Person> Persons
        {
            get
            {
                if (Persons == null)
                    Persons = new List<Person>();

                return Persons;
            }
            set { Persons = value; }
        }
        public RelationType RelationTypeId { get; set; }
    }
}