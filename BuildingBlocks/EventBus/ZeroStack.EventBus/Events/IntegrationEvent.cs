using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTimeOffset.Now;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationTime = createDate;
        }

        public Guid Id { get; set; }

        public DateTimeOffset CreationTime { get; set; }
    }
}
