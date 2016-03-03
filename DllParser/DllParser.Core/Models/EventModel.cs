using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class EventModel : BaseModel
    {
        public override string Description
        {
            get
            {
                return string.Format("{0}", Name);
            }
        }

        public override bool IsHasChild
        {
            get
            {
                return false;
            }
        }

        public EventModel(EventInfo type)
            : base(ModelType.Event)
        {
            Name = type.Name;
        }
    }
}
