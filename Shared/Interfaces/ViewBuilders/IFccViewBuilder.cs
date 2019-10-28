﻿using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;
using Shared.Enums;
using Shared.Interfaces.Managers;

namespace Shared.Interfaces.ViewBuilders
{
    public interface IFccViewBuilder
    {
        void HandleAction(PersonViewModel vm);
        List<PersonName> CreatePartialViewForNamesAndPatronymList(string personId);
        PersonPartialViewRelationsModel CreatePersonPartialViewRelationsModel(string personId);
        KeyValuePair<Person, Person> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId);
    }
}