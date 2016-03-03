using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Entities
{
    public class EventEntity : BaseEntity
    {
        private string _eventHandlerType;

        public override string Description
        {
            get
            {
                return string.Format("{0} : {1}", Name, _eventHandlerType);
            }
        }

        public override bool IsHasChild
        {
            get
            {
                return false;
            }
        }

        public EventEntity(EventInfo type)
            : base(ModelType.Event)
        {
            Name = type.Name;
            _eventHandlerType = type.EventHandlerType.Name;
        }
    }
}
