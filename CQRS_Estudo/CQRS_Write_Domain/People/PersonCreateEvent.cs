using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonCreateEvent : Event
    {
        public PersonClass Class { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public PersonCreateEvent(int aggregateId, PersonClass personClass, string nome, int idade)
        {
            AggregateId = aggregateId;
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }
    }
}
