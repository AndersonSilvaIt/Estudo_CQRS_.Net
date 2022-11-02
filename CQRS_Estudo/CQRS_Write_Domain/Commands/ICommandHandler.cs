namespace CQRS_Write_Domain.Commands
{
    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Handler(T command);
    }

    public interface ICommandHandler { }
}
