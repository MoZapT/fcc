using WAFcc.Enums;
using WAFcc.Models;

namespace WAFcc.Viewmodels
{
    public class PersonViewModel : BaseViewModel
    {
        private VmState _viewState;

        public Person Person { get; set; }
        public IEnumerable<Person> Persons { get; set; }

        public async Task Init()
        {
            _viewState = VmState.List;
            Skip = 0;
            Take = 10;
            Person = null;
        }

        public void CreatePerson()
        {

        }

        public void EditPerson()
        {

        }
    }
}