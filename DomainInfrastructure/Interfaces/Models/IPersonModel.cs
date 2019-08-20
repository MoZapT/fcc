using DomainInfrastructure.Common;
using DomainInfrastructure.Entity;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Models.Family;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Models
{
    public interface IPersonModel : IBaseModel
    {
        string Name { get; set; }
        string Lastname { get; set; }
        string Patronym { get; set; }
        bool NameModified { get; set; }
        [Display(Name = "Д.Р. известна")]
        bool BornTimeKnown { get; set; }
        [Display(Name = "Умер(ла)")]
        bool IsDead { get; set; }
        [Display(Name = "Дата рождения")]
        DateTime BornTime { get; set; }
        [Display(Name = "Дата смерти")]
        DateTime DeadTime { get; set; }
        [Display(Name = "Пол")]
        bool Sex { get; set; }
    }
}
