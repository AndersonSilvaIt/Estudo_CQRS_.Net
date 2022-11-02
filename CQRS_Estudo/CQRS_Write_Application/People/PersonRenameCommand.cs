using CQRS_Write_Domain.Commands;

namespace CQRS_Write_Application.People
{
    public class PersonRenameCommand : Command
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public PersonRenameCommand(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
