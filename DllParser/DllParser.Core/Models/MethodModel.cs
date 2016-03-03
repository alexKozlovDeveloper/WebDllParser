using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class MethodModel : BaseModel
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

        public MethodModel(MethodInfo type)
            : base(ModelType.Method)
        {
            Name = type.Name;
        }

        public MethodModel(ConstructorInfo type)
            : base(ModelType.Method)
        {
            Name = type.Name;
        }
    }
}
