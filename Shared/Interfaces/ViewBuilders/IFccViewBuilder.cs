using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;
using Shared.Enums;
using Shared.Interfaces.Managers;
using System;

namespace Shared.Interfaces.ViewBuilders
{
    public interface IFccViewBuilder
    {
        void HandleAction(PersonViewModel vm);
        PersonDocumentsViewModel CreatePartialViewPersonDocuments(string personId);
        KeyValuePair<string, List<FileContent>> CreatePartialViewPersonPhotos(string personId);
        PersonBiographyViewModel CreatePartialViewPersonBiography(string personId);
        bool SavePersonActivity(string personId, string bioId, PersonActivity newact);
        List<PersonName> CreatePartialViewForNamesAndPatronymList(string personId);
        PersonRelationsViewModel CreatePersonPartialViewRelationsModel(string personId);
        Tuple<Person, Person, Person> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId, string partnerId);
        RelationsUpdateStackViewModel CreateUpdateRelationsStackViewModel(string personId);
    }
}
