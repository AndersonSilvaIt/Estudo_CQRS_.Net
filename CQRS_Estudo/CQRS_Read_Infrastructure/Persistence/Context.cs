using CQRS_Read_Infrastructure.Persistence.People;

namespace CQRS_Read_Infrastructure.Persistence
{
    public class Context : IContext
    {
        public IPersonRepository People { get; set; }

        public Context(IPersonRepository personRepository)
        {
            People = personRepository;
        }
    }
}
