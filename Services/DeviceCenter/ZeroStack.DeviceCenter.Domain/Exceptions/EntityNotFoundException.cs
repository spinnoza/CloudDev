using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStack.DeviceCenter.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; set; }

        public EntityNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }

        public override string ToString()
        {
            return $"There is no such an entity given given id. Entity type: {EntityType.FullName}";
        }
    }
}
