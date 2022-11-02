using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.Commands
{
    public interface ICommandBus : ICommandSender, IEventPublisher
    {

    }
}
