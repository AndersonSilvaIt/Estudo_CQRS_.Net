using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CQRS_Read_Infrastructure.Persistence.People
{
    [Flags]
    public enum PersonClass
    {
        Comun,
        Admin
    }

    public class Person
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PersonClass Class { get; set; }

        public string Nome { get; set; }
        public int Idade { get; set; }

        public Person(int id, PersonClass personClass, string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException("nome");

            Id = id;
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }

        public Person(PersonClass personClass, string nome, int idade)
        {
            Id = -1;
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }

        public override string ToString()
        {
            return $"{Class} : [Class]{Class}, [Nome]{Nome}, [Idade]{Idade}";
        }
    }
}
