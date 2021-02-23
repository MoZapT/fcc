using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppFcc.Shared.Models
{
    public class PersonRelation
    {
        public string RelationId { get; set; }
        public string PersonId { get; set; }
    }
}