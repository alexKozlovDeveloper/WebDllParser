using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Models
{
    public class TypeModel
    {
        public string Name { get; set; }
        public string Namespace { get; set; }

        public List<EventModel> Events { get; set; }
        public List<FieldModel> Fields { get; set; }
        public List<MethodModel> Methods { get; set; }
        public List<PropertyModel> Properties { get; set; }

        public TypeModel(TypeInfo type)
        {
            Name = type.Name;
            Namespace = type.Namespace;

            Events = type.DeclaredEvents.Select(a => new EventModel(a)).ToList();
            Fields = type.DeclaredFields.Select(a => new FieldModel(a)).ToList();
            Properties = type.DeclaredProperties.Select(a => new PropertyModel(a)).ToList();
            Methods = type.DeclaredMethods.Select(a => new MethodModel(a)).ToList();            
            Methods.AddRange(type.DeclaredConstructors.Select(a => new MethodModel(a)).ToList());
        }
    }
}
