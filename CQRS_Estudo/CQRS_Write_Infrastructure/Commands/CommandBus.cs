using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;
using System.Reflection;

namespace CQRS_Write_Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private static Dictionary<Type, List<Action<ICommand>>> commandHandlerDisctionary = new Dictionary<Type, List<Action<ICommand>>>();
        private static Dictionary<Type, List<Action<IEvent>>> eventHandlerDisctionary = new Dictionary<Type, List<Action<IEvent>>>();

        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IEvent>> eventActions;
            if (eventHandlerDisctionary.TryGetValue(@event.GetType(), out eventActions))
            {
                foreach (var eventHandlerMethod in eventActions)
                {
                    eventHandlerMethod(@event);
                }
            }
            else
            {
                throw new InvalidOperationException($"Evento não foi registrado {@event}");
            }
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<ICommand>> commandActions;
            if (commandHandlerDisctionary.TryGetValue(command.GetType(), out commandActions))
            {
                foreach (var comandHandlerMethod in commandActions)
                {
                    comandHandlerMethod(command);
                }
            }
            else
            {
                throw new InvalidOperationException($"Comando não foi registrado {command}");
            }
        }

        public void RegisterCommandHandlers(ICommandHandler commandHandler)
        {
            var commandHandlerMethod = commandHandler.GetType().GetMethods()
                .Where(m => m.GetParameters()
                .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))));

            foreach (var method in commandHandlerMethod)
            {
                ParameterInfo commandParameterInfo = method.GetParameters()
                    .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))).FirstOrDefault();

                if (commandParameterInfo == null) continue;

                Type commandParameterType = commandParameterInfo.ParameterType;

                List<Action<ICommand>> commandActions;

                if (!commandHandlerDisctionary.TryGetValue(commandParameterType, out commandActions))
                {
                    commandActions = new List<Action<ICommand>>();
                    commandHandlerDisctionary.Add(commandParameterType, commandActions);
                }

                commandActions.Add(x => method.Invoke(commandHandler, new object[] { x }));
            }
        }

        public void RegisterEventHandlers(IEventHandler eventHandlers)
        {
            var commandHandlerMethod = eventHandlers.GetType().GetMethods()
                                      .Where(m => m.GetParameters()
                                      .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))));

            foreach (var method in commandHandlerMethod)
            {
                ParameterInfo commandParameterInfo = method.GetParameters()
                    .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))).FirstOrDefault();

                if (commandParameterInfo == null) continue;

                Type commandParameterType = commandParameterInfo.ParameterType;

                List<Action<IEvent>> eventActions;

                if (!eventHandlerDisctionary.TryGetValue(commandParameterType, out eventActions))
                {
                    eventActions = new List<Action<IEvent>>();
                    eventHandlerDisctionary.Add(commandParameterType, eventActions);
                }

                eventActions.Add(x => method.Invoke(eventHandlers, new object[] { x }));
            }
        }
    }
}