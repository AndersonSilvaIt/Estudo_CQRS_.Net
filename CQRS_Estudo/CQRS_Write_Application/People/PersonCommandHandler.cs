using CQRS_Read_Aplication.People;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;

namespace CQRS_Write_Application.People
{
    public class PersonCommandHandler : ICommandHandler<PersonCreateCommand>, 
                                        ICommandHandler<PersonDeleteCommand>, 
                                        ICommandHandler<PersonRenameCommand>
    {
        private readonly ICommandEventRepository _commandEventRepository;
        private readonly IPersonService _personService;

        public PersonCommandHandler(IPersonService personService, ICommandEventRepository commandEventRepository)
        {
            _commandEventRepository = commandEventRepository;
            _personService = personService;
        }

        public void Handler(PersonCreateCommand command)
        {
            var id = _personService.GetAll().Count() + 1;

            Person person = new Person(id, command.Class, command.Nome, command.Idade);

            _commandEventRepository.Save(person);
        }

        public void Handler(PersonDeleteCommand command)
        {
            Person person = _commandEventRepository.GetByCommandId<Person>(command.Id);
            person.Delete();

            _commandEventRepository.Save(person);
        }

        public void Handler(PersonRenameCommand command)
        {
            Person person = _commandEventRepository.GetByCommandId<Person>(command.Id);
            person.Rename(command.Nome);

            _commandEventRepository.Save(person);
        }
    }
}
