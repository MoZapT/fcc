﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using WebAppFcc.Shared.Interfaces.DataServices;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace WebAppFcc.Data.DataServices
{
    public class BaseDataService : IBaseDataService
    {
        public event Action OnChange;
        public HttpClient Http { get; }

        public BaseDataService(HttpClient http)
        {
            Http = http;
        }

        protected void NotifyStateChanged() => OnChange?.Invoke();

        public virtual void Init(Action action)
        {
            OnChange += action;
        }

        protected async Task DefaultApiRequest(Func<Task> tryExecute, Action<Exception> onError = null)
        {
            try
            {
                await tryExecute.Invoke();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();

                if (onError != null)
                    onError.Invoke(ex);
            }
            catch (Exception ex)
            {
                if (onError != null)
                    onError.Invoke(ex);
            }
            finally
            {
                NotifyStateChanged();
            }
        }
    }
}
