using CQRS_Estudo.Models;
using CQRS_Read_Aplication.People;
using CQRS_Read_Infrastructure.Persistence;
using CQRS_Read_Infrastructure.Persistence.People;
using CQRS_Write_Application.People;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_Estudo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ICommandBus commandBus;
        private IPersonService personService;
        private ICommandEventRepository commandEventRepository;
        private IContext context;
        private IPersonRepository personRepository;

        public HomeController(ILogger<HomeController> logger,
           ICommandBus commandBus, IPersonService personService, ICommandEventRepository commandEventRepository
           , IPersonRepository personRepository)
        {
            _logger = logger;
            this.commandBus = commandBus;
            this.personService = personService;
            this.commandEventRepository = commandEventRepository;
            this.personRepository = personRepository;
            this.context = new Context(personRepository);
        }


        public IActionResult Index()
        {
            commandBus.RegisterCommandHandlers(new PersonCommandHandler(personService, commandEventRepository));
            commandBus.RegisterEventHandlers(new PersonEventHandler(personService));

            commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Admin, "Valdir Ferreira", 38));
            commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Admin, "Beatriz", 5));
            commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Comum, "Usuário Canal", 25));
            commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Comum, "Maluco Beleza", 50));

            var lista = personRepository.Get();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}