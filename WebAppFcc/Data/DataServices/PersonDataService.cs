using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using WebAppFcc.Shared.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using WebAppFcc.Shared.Interfaces.DataServices;
using WebAppFcc.Shared.Enums;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace WebAppFcc.Data.DataServices
{
    public class PersonDataService : IPersonDataService
    {
        public event Action OnChange;
        public HttpClient Http { get; }

        public VmState ViewState { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public Person Person { get; set; }
        public IEnumerable<Person> Persons { get; set; }

        public PersonDataService(HttpClient http)
        {
            Http = http;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void Init(Action action)
        {
            ViewState = VmState.List;
            Skip = 0;
            Take = 10;
            Person = null;
            Persons = new List<Person>();

            OnChange += action;
        }

        public async Task LoadPersonList()
        {
            try
            {
                var response = await Http.GetAsync("family/person/get-list");
                Persons = await response.Content.ReadFromJsonAsync<IEnumerable<Person>>();
                ViewState = VmState.List;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                Persons = new List<Person>();
            }
            catch (Exception)
            {
                Persons = new List<Person>();
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public async Task LoadPersonDetails(Guid id)
        {
            try
            {
                var response = await Http.GetAsync($"family/person/get/{id}");
                Person = await response.Content.ReadFromJsonAsync<Person>();
                ViewState = VmState.Detail;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                Person = null;
            }
            catch (Exception)
            {
                Person = null;
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public async Task DeletePerson(Guid id)
        {
            try
            {
                var response = await Http.DeleteAsync($"family/person/delete/{id}");
                await response.Content.ReadFromJsonAsync<bool>();
                ViewState = VmState.Detail;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception)
            {
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public async Task AddPerson(Person person)
        {
            try
            {
                var response = await Http.PostAsJsonAsync($"family/person/add/{person}", person);
                await response.Content.ReadFromJsonAsync<Person>();
                ViewState = VmState.Detail;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception)
            {
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public async Task UpdatePerson(Person person)
        {
            try
            {
                var response = await Http.PutAsJsonAsync($"family/person/update/{person}", person);
                await response.Content.ReadFromJsonAsync<Person>();
                ViewState = VmState.Detail;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception)
            {
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public void CreatePerson()
        {
            Person = new Person();
            ViewState = VmState.Detail;

            NotifyStateChanged();
        }
    }
}
