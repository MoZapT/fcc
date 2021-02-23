﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Collections.Generic;
using WebAppFcc.Shared.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using WebAppFcc.Shared.Interfaces.DataServices;
using WebAppFcc.Shared.Enums;
using System.Net.Http;
using System;

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
        }

        public async Task LoadPersonDetails(string id)
        {
            try
            {
                var response = await Http.GetAsync($"family/person/{id}");
                Person = await response.Content.ReadFromJsonAsync<Person>();
                ViewState = VmState.Detail;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                Person = null;
            }
        }

        public async Task DeletePerson(string id)
        {
            var response = await Http.DeleteAsync($"family/person/delete/{id}");
            await response.Content.ReadFromJsonAsync<bool>();
            ViewState = VmState.Detail;
        }

        public async Task AddPerson(Person person)
        {
            var response = await Http.PostAsJsonAsync($"family/person/add/", person);
            await response.Content.ReadFromJsonAsync<Person>();
            ViewState = VmState.Detail;
        }

        public async Task UpdatePerson(Person person)
        {
            var response = await Http.PutAsJsonAsync($"family/person/update/", person);
            await response.Content.ReadFromJsonAsync<Person>();
            ViewState = VmState.Detail;
        }

        public void CreatePerson()
        {
            Person = new Person();
            ViewState = VmState.Detail;

            NotifyStateChanged();
        }
    }
}
