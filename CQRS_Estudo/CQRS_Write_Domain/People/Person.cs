namespace CQRS_Write_Domain.People
{
    public class Person : AggregateRoot<int>
    {
        public PersonType Class { get; protected set; }

        public Person() { }

        public Person(int id, PersonClass personClass, string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException("nome");

            ApplyChange(new PersonCreateEvent(id, personClass, nome, idade));
        }

        public Person(PersonClass personClass, string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException("nome");

            ApplyChange(new PersonCreateEvent(0, personClass, nome, idade));
        }

        public void Rename(string nome)
        {
            ApplyChange(new PersonRenamedEvent(this.Id, nome));
        }

        public void Delete()
        {
            ApplyChange(new PersonDeleteEvent(this.Id));
        }

        private void Apply(PersonRenamedEvent @event)
        {
            Id = @event.AggregateId;
            Class = new PersonType(Class.Class, @event.Nome, Class.Idade);
        }

        public override string ToString()
        {
            return $"{Class.Nome} : [Class]{Class}, [Nome]:{Class.Nome}, [Idade]:{Class.Idade}";
        }

    }
}
