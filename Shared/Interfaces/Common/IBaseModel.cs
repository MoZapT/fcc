using System;

namespace Shared.Interfaces.Common
{
    public interface IBaseModel
    {
        string Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        bool IsActive { get; set; }
    }
}
