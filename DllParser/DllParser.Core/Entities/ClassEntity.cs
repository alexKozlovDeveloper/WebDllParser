using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Entities
{
    public class ClassEntity : BaseEntity
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

        public ClassEntity(string name)
            : base(ModelType.Class)
        {
            Name = name;
        }
    }
}
