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
    public class PersonDataService : BaseDataService, IPersonDataService
    {
        public VmState ViewState { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int PersonCount { get; set; }

        public Person Person { get; set; }
        public IEnumerable<Person> Persons { get; set; }

        public PersonDataService(HttpClient http) : base(http)
        {
        }

        public override void Init(Action action)
        {
            ViewState = VmState.List;
            Skip = 0;
            Take = 10;
            PersonCount = 0;
            Person = null;
            Persons = new List<Person>();

            base.Init(action);
        }

        public async Task LoadPersonList(bool resetSkip = false)
        {
            if (resetSkip)
                Skip = 0;

            Func<Task> executeGetList = new Func<Task>(async () => {
                var response = await Http.GetAsync($"family/person/get-list/{Skip}/{Take}");
                Persons = await response.Content.ReadFromJsonAsync<IEnumerable<Person>>();
                ViewState = VmState.List;
            });
            Action<Exception> onError = new Action<Exception>((Exception ex) => {
                Persons = new List<Person>();
            });

            await DefaultApiRequest(executeGetList, onError);

            Func<Task> executeCount = new Func<Task>(async () => {
                var response = await Http.GetAsync($"family/person/get-count");
                PersonCount = await response.Content.ReadFromJsonAsync<int>();
            });

            await DefaultApiRequest(executeCount);
        }

        public async Task LoadPersonDetails(Guid id)
        {
            Func<Task> tryExecute = new Func<Task>(async () => {
                var response = await Http.GetAsync($"family/person/get/{id}");
                Person = await response.Content.ReadFromJsonAsync<Person>();
                ViewState = VmState.Detail;
            });
            Action<Exception> onError = new Action<Exception>((Exception ex) => {
                Person = null;
            });

            await DefaultApiRequest(tryExecute, onError);
        }

        public async Task DeletePerson(Guid id)
        {
            Func<Task> tryExecute = new Func<Task>(async () => {
                var response = await Http.DeleteAsync($"family/person/delete/{id}");
                await response.Content.ReadFromJsonAsync<Person>();
                await LoadPersonList();
            });

            await DefaultApiRequest(tryExecute);
        }

        public async Task AddPerson(Person person)
        {
            Func<Task> tryExecute = new Func<Task>(async () => {
                var response = await Http.PostAsJsonAsync($"family/person/add/{person}", person);
                await response.Content.ReadFromJsonAsync<Person>();
                await LoadPersonList();
            });

            await DefaultApiRequest(tryExecute);
        }

        public async Task UpdatePerson(Person person)
        {
            Func<Task> tryExecute = new Func<Task>(async () => {
                var response = await Http.PutAsJsonAsync($"family/person/update/{person}", person);
                await response.Content.ReadFromJsonAsync<Person>();
                await LoadPersonList();
            });

            await DefaultApiRequest(tryExecute);
        }

        public void CreatePerson()
        {
            Person = new Person();
            ViewState = VmState.Detail;

            NotifyStateChanged();
        }
    }
}
