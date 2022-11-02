namespace CQRS_Write_Domain.Events
{
    public interface IEventHandler<T> : IEventHandler where T : IEvent
    {
        void Handle(T @event);
    }

    public interface IEventHandler { }
}
