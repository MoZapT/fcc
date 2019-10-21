using Shared.Common;
using Shared.Enums;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public class PersonRelationChain : BaseModel
    {
        private string _parentsId;
        private string _childrenId;

        public string ParentsId { get { return _parentsId; } }
        public string ChildrenId { get { return _childrenId; } }
        public PersonRelationGroup Parents
        {
            get { return Parents; }
            set { Parents = value; _parentsId = value.Id; }
        }
        public PersonRelationGroup Children
        {
            get { return Children; }
            set { Children = value; _childrenId = value.Id; }
        }
    }
}