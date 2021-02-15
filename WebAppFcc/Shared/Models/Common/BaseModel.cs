using WebAppFcc.Shared.Interfaces.Common;
using System;

namespace WebAppFcc.Shared.Common
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
    }
}