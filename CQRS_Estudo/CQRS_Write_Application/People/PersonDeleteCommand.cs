using CQRS_Write_Domain.Commands;

namespace CQRS_Write_Application.People
{
    public class PersonDeleteCommand : Command
    {
        public int Id { get; set; }

        public PersonDeleteCommand(int id)
        {
            Id = id;
        }
    }
}
