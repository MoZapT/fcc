﻿using Shared.Interfaces.Common;
using System;

namespace Shared.Common
{
    public class BaseModel : IBaseModel
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
    }
}