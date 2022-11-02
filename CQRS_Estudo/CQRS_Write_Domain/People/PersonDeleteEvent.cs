using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonDeleteEvent : Event
    {
        public PersonDeleteEvent(int aggregateId) : base()
        {
            AggregateId = aggregateId;
        }
    }
}
