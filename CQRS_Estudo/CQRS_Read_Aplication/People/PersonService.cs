using CQRS_Read_Infrastructure.Persistence.People;

namespace CQRS_Read_Aplication.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void Delete(object id)
        {
            _personRepository.Delete(id);
        }

        public Person Find(object id)
        {
            return _personRepository.Find(id);
        }

        public IQueryable<Person> GetAll()
        {
            return _personRepository.Get();
        }

        public IQueryable<Person> GetByName(string name)
        {
            return _personRepository.Get(p => p.Nome.ToUpper().Contains(name.ToUpper()));
        }

        public void Insert(Person entity)
        {
            _personRepository.Insert(entity);
        }

        public void Update(Person entity)
        {
            _personRepository.Update(entity);
        }
    }
}
