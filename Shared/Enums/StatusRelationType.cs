using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum StatusRelationType
    {
        [Description("Husband|Wife")]
        HusbandWife,
        [Description("LivePartnerMale|LivePartnerFemale")]
        LivePartner,
    }
}
