using Shared.Interfaces.Common;
using System;

namespace Shared.Common
{
    public class BaseModel : IBaseModel
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }

        public BaseModel()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            IsActive = true;
        }

        public bool IsNull()
        {
            if (this == null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(Id))
            {
                return true;
            }

            return false;
        }
    }
}