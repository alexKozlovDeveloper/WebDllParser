using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Entities
{
    public class PropertyEntity : BaseEntity
    {
        private string _type;

        public override string Description
        {
            get
            {
                return string.Format("{0} : {1}", Name, _type);
            }
        }

        public override bool IsHasChild
        {
            get
            {
                return false;
            }
        }

        public PropertyEntity(PropertyInfo type)
            : base(ModelType.Property)
        {
            Name = type.Name;
            _type = type.PropertyType.Name;
        }
    }
}
