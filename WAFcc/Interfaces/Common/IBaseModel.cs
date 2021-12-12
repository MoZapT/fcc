using System;

namespace WAFcc.Interfaces.Common
{
    public interface IBaseModel
    {
        Guid Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        bool IsActive { get; set; }
    }
}
