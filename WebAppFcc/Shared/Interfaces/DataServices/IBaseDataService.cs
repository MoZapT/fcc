using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IBaseDataService 
    {
        event Action OnChange;
        HttpClient Http { get; }

        void Init(Action action);
    }
}
