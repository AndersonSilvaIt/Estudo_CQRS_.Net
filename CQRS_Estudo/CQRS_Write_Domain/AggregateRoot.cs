using CQRS_Write_Domain.Events;
using System.Reflection;

namespace CQRS_Write_Domain
{
    public class AggregateRoot<T> : IAggregateRoot<T>
    {
        private List<IEvent> eventChanges = new List<IEvent>();

        public T Id { get; protected set; }

        public int Version { get; protected set; }

        public void ApplyChange(IEvent @event, bool isNew = true)
        {
            var method = this.GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { @event.GetType() }, null);

            if(method != null)
            {
                method.Invoke(this, new object[] { @event });
            }

            if (isNew)
            {
                @event.Version = eventChanges.Count() + 1;
                @event.Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
                eventChanges.Add(@event);
            }
        }

        public object GetId()
        {
            return Id;
        }

        public IEnumerable<IEvent> GetUncommitChanges()
        {
            return this.eventChanges;
        }

        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var @event in history)
            {
                this.ApplyChange(@event, false);
            }
        }

        public void MarkChangesAsCommited()
        {
            eventChanges.Clear();
        }
    }
}
