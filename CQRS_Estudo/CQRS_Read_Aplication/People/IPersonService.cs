using CQRS_Read_Infrastructure.Persistence.People;

namespace CQRS_Read_Aplication.People
{
    public interface IPersonService : IApplicationService<Person>
    {
        Person Find(object id);
        IQueryable<Person> GetByName(string name);
        IQueryable<Person> GetAll();

        void Insert(Person entity);
        void Update(Person entity);
        void Delete(object id);
    }
}
