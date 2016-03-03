using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    
    public class NamespaceModel : BaseModel
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

        public NamespaceModel(string name)
            : base(ModelType.Namespace)
        {
            Name = name;
        }
    }
}
