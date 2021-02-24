namespace WebAppFcc.Shared.Models
{
    public class PersonRelation
    {
        public Relation Relation { get; set; }
        public string RelationId { get; set; }
        public Person Person { get; set; }
        public string PersonId { get; set; }
    }
}