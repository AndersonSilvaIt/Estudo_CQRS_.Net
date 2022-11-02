using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;

namespace CQRS_Write_Application.People
{
    public class PersonCreateCommand : Command
    {
        public PersonClass Class { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public PersonCreateCommand(PersonClass @class, string nome, int idade)
        {
            Class = @class;
            Nome = nome;
            Idade = idade;
        }
    }
}
