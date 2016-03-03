using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Entities
{
    public class NamespaceEntity : BaseEntity
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
                return true;
            }
        }

        public NamespaceEntity(string name)
            : base(ModelType.Namespace)
        {
            Name = name;
        }
    }
}
