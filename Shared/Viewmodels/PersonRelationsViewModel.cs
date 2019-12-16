using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonRelationsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Person { get; set; }
        public Dictionary<RelationType, List<Person>> Relations { get; set; }
        public List<RelationType> RelationTypeLoadingList { set; get; }

        #endregion

        public PersonRelationsViewModel()
        {
            InitRelationsDictionaryList();
        }

        private void InitRelationsDictionaryList()
        {
            Relations = new Dictionary<RelationType, List<Person>>();
            RelationTypeLoadingList = new List<RelationType>();
            //TODO divorced wife
            //TODO previous live partner
            //TODO wife
            //TODO live partner
            RelationTypeLoadingList.Add(RelationType.FatherMother);
            RelationTypeLoadingList.Add(RelationType.BrotherSister);
            RelationTypeLoadingList.Add(RelationType.SonDaughter);
            RelationTypeLoadingList.Add(RelationType.StepFatherMother);
            RelationTypeLoadingList.Add(RelationType.StepBrotherSister);
            RelationTypeLoadingList.Add(RelationType.StepSonDaughter);
            RelationTypeLoadingList.Add(RelationType.InLawFatherMother);
            RelationTypeLoadingList.Add(RelationType.InLawBrotherSister);
            RelationTypeLoadingList.Add(RelationType.InLawSonDaughter);
            RelationTypeLoadingList.Add(RelationType.UncleAunt);
            RelationTypeLoadingList.Add(RelationType.Cousins);
            RelationTypeLoadingList.Add(RelationType.NephewNiece);
            RelationTypeLoadingList.Add(RelationType.GrandFatherMother);
            RelationTypeLoadingList.Add(RelationType.GrandSonDaughter);
            RelationTypeLoadingList.Add(RelationType.Siblings);
            RelationTypeLoadingList.Add(RelationType.InLawSiblings);
        }

    }
}