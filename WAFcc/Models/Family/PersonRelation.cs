namespace WAFcc.Models
{
    public class PersonRelation : BaseModel
    {
        public Relation Relation { get; set; }
        public Guid RelationId { get; set; }
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}