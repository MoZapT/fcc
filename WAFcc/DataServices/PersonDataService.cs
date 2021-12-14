using WAFcc.Enums;
using WAFcc.Interfaces.DataServices;
using WAFcc.Interfaces.Managers;
using WAFcc.Models;

namespace WAFcc.DataServices
{
    public class PersonDataService : BaseDataService, IPersonDataService
    {
        public VmState ViewState { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int PersonCount { get; set; }

        public Person Person { get; set; }
        public IEnumerable<Person> Persons { get; set; }

        IFccManager _fccMgr;

        public PersonDataService(IFccManager mgr) : base()
        {
            _fccMgr = mgr;
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

            try
            {
                Persons = await _fccMgr.GetPersonList(Skip, Take);
                ViewState = VmState.List;
                PersonCount = await _fccMgr.PersonCount();
            }
            catch (Exception e)
            {
                Persons = new List<Person>();
                PersonCount = 0;
            }
        }

        public async Task LoadPersonDetails(Guid id)
        {
            try
            {
                Person = await _fccMgr.GetPerson(id);
                ViewState = VmState.Detail;
                NotifyStateChanged();
            }
            catch (Exception e)
            {
                Person = null;
            }
        }

        public async Task DeletePerson(Guid id)
        {
            await _fccMgr.DeletePerson(id);
            await LoadPersonList();
        }

        public async Task AddPerson(Person person)
        {
            await _fccMgr.CreatePerson(person);
        }

        public async Task UpdatePerson(Person person)
        {
            await _fccMgr.UpdatePerson(person);
        }

        public void CreatePerson()
        {
            Person = new Person();
            ViewState = VmState.Detail;

            NotifyStateChanged();
        }
    }
}
