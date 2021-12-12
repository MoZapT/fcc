using WAFcc.Models;
using WAFcc.Viewmodels;

namespace WAFcc.Interfaces.ViewBuilders
{
    public interface IFccViewBuilder
    {
        Task HandleAction(PersonViewModel vm);
        Task<IEnumerable<ActivityDocumentsViewModel>> CreatePartialViewPersonDocuments(string personId);
        Task<KeyValuePair<string, IEnumerable<FileContent>>> CreatePartialViewPersonPhotos(string personId);
        Task<PersonBiographyViewModel> CreatePartialViewPersonBiography(string personId);
        Task<bool> SavePersonActivity(string personId, string bioId, PersonActivity newact);
        Task<IEnumerable<PersonName>> CreatePartialViewForNamesAndPatronymList(string personId);
        Task<PersonRelationsViewModel> CreatePersonPartialViewRelationsModel(string personId);
        Task<Tuple<Person, Person, Person>> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId, string partnerId);
        Task<RelationsUpdateStackViewModel> CreateUpdateRelationsStackViewModel(string personId, string selectedId);
        Task<IEnumerable<PersonRelation>> CreateRelationsUpdateStackPartial(string personId, string selectedId);
        Task<PersonActivity> GetPersonActivity(string activityId);
    }
}
