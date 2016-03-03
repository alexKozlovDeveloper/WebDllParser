using DllParser.Core.Entities;
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

        public List<EventEntity> Events { get; set; }
        public List<FieldEntity> Fields { get; set; }
        public List<MethodEntity> Methods { get; set; }
        public List<PropertyEntity> Properties { get; set; }

        public TypeModel(TypeInfo type)
        {
            Name = type.Name;
            Namespace = type.Namespace;

            Events = type.DeclaredEvents.Select(a => new EventEntity(a)).ToList();
            Fields = type.DeclaredFields.Select(a => new FieldEntity(a)).ToList();
            Properties = type.DeclaredProperties.Select(a => new PropertyEntity(a)).ToList();
            Methods = type.DeclaredMethods.Select(a => new MethodEntity(a)).ToList();            
            Methods.AddRange(type.DeclaredConstructors.Select(a => new MethodEntity(a)).ToList());
        }
    }
}
