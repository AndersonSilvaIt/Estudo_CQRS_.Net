using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonRenamedEvent : Event
    {
        public string Nome { get; }

        public PersonRenamedEvent(int aggregateId, string name) : base()
        {
            AggregateId = aggregateId;
            Nome = name;
        }
    }
}
