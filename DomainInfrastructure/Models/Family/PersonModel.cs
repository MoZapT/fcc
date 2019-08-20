using DomainInfrastructure.Common;
using DomainInfrastructure.Entity;
using DomainInfrastructure.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomainInfrastructure.Models.Family
{
    public class PersonModel : BaseModel, IPersonModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public bool NameModified { get; set; }
        [Display(Name = "Д.Р. известна")]
        public bool BornTimeKnown { get; set; }
        [Display(Name = "Умер(ла)")]
        public bool IsDead { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime BornTime { get; set; }
        [Display(Name = "Дата смерти")]
        public DateTime DeadTime { get; set; }
        [Display(Name = "Пол")]
        public bool Sex { get; set; }
    }
}