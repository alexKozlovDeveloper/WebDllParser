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
        public string TypeName { get; set; }
        public string ParametrsInfo { get; set; }
        public Guid Id { get; set; }

        public List<TypeModel> Constructors { get; set; }
        public List<TypeModel> Events { get; set; }
        public List<TypeModel> Fields { get; set; }
        public List<TypeModel> Methods { get; set; }
        public List<TypeModel> Properties { get; set; }

        public List<TypeModel> FieldsChilds { get; set; }
        public List<TypeModel> PropertiesChilds { get; set; }

        public bool IsHasChild
        {
            get
            {
                return Constructors.Count > 0 
                    || Events.Count > 0
                    || Fields.Count > 0
                    || Methods.Count > 0
                    || Properties.Count > 0;
            }
        }

        public TypeModel()
        {
            ParametrsInfo = string.Empty;
            Constructors = new List<TypeModel>();
            Events = new List<TypeModel>();
            Fields = new List<TypeModel>();
            Methods = new List<TypeModel>();
            Properties = new List<TypeModel>();
        }

        public TypeModel(TypeInfo type)
        {
            Name = type.Name;
            TypeName = type.Name;

            ParametrsInfo = string.Empty;

            Constructors = type.DeclaredConstructors.Select(a => new TypeModel(a.Name)).ToList();
            Events = type.DeclaredEvents.Select(a => new TypeModel(a.Name, a.RemoveMethod.ReturnType.Name)).ToList();
            Fields = type.DeclaredFields.Select(a => new TypeModel(a.Name, a.FieldType.Name)).ToList();
            Methods = type.DeclaredMethods.Select(a => new TypeModel(a.Name, a.ReturnType.Name, a.GetParameters())).ToList();
            Properties = type.DeclaredProperties.Select(a => new TypeModel(a.Name, a.PropertyType.Name)).ToList();
        }

        public TypeModel(string name, string type = "", ParameterInfo[] parametars = null)
        {
            Name = name;
            TypeName = type;

            ParametrsInfo = parametars != null ? "(" + string.Join(", ", parametars.Select(a => a.ParameterType.Name)) + ")" : string.Empty;

            Constructors = new List<TypeModel>();
            Events = new List<TypeModel>();
            Fields = new List<TypeModel>();
            Methods = new List<TypeModel>();
            Properties = new List<TypeModel>();
        }

        public override bool Equals(object obj)
        {
            TypeModel item = obj as TypeModel;
            return item.Name == this.Name;
        }
    }
}
