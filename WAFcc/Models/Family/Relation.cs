using WAFcc.Enums;

namespace WAFcc.Models
{
    public class Relation : BaseModel
    {
        public ICollection<Person> Members { get; set; }
        public ICollection<PersonRelation> PersonRelations { get; set; }
        public RelationType RelationType { get; set; }

        public Person GetMember(Guid excludeId)
        {
            return Members?.FirstOrDefault(e => e.Id != excludeId);
        }
    }
}