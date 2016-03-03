using DllParser.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Entities
{
    public class MethodEntity : BaseEntity
    {
        private string _returnType;
        private string _parameters;

        public override string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_returnType) == false)
                {
                    return string.Format("{0}({1}) : {2}", Name, _parameters, _returnType);
                }
                else
                {
                    return string.Format("{0}({1})", Name, _parameters);
                }
            }
        }

        public override bool IsHasChild
        {
            get
            {
                return false;
            }
        }

        public MethodEntity(MethodInfo type)
            : base(ModelType.Method)
        {
            Name = type.Name;
            _returnType = type.ReturnType.Name;
            var array = type.GetParameters().Select(a => a.ParameterType.Name).ToArray();

            _parameters = String.Join(", ", array);
        }

        public MethodEntity(ConstructorInfo type)
            : base(ModelType.Method)
        {
            Name = type.Name;
        }
    }
}
