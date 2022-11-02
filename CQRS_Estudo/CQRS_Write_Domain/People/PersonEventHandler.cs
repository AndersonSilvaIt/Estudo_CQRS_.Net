using CQRS_Read_Aplication.People;
using CQRS_Read_Infrastructure.Persistence.People;
using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonEventHandler : IEventHandler<PersonCreateEvent>, IEventHandler<PersonDeleteEvent>, IEventHandler<PersonRenamedEvent>
    {
        private readonly IPersonService _personService;

        public PersonEventHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public void Handle(PersonCreateEvent @event)
        {
            Person person = new Person(@event.AggregateId, (CQRS_Read_Infrastructure.Persistence.People.PersonClass)@event.Class, @event.Nome, @event.Idade);

            _personService.Insert(person);
        }

        public void Handle(PersonRenamedEvent @event)
        {
            var person = _personService.Find(@event.AggregateId);
            person.Nome = @event.Nome;

            _personService.Update(person);
        }

        public void Handle(PersonDeleteEvent @event)
        {
            _personService.Delete(@event.AggregateId);
        }
    }
}
