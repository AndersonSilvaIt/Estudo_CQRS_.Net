namespace CQRS_Write_Domain.Commands
{
    public class Command : ICommand
    {
        public string Type
        {
            get { return this.GetType().Name; }
        }
    }
}
