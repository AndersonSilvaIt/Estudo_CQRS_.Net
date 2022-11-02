using CQRS_Read_Infrastructure.Persistence.People;

namespace CQRS_Read_Infrastructure.Persistence
{
    public interface IContext
    {
        IPersonRepository People { get; set; }
    }
}
