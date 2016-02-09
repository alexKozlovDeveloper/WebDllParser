using DllParser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core.Helpers
{
    class AssemblyHelper
    {
        public static TypeModel GetTypeModel(TypeInfo type)
        {
            TypeModel model = new TypeModel 
            {
                Name = type.Name,
                Id = type.GUID
            };

            model.Functions = type.DeclaredMethods.Select(a => a.Name).ToList();
            model.Events = type.DeclaredEvents.Select(a => a.Name).ToList();
            model.ChildsTypeName = type.DeclaredProperties.Select(a => a.PropertyType.Name).ToList();

            return model;
        }

        public static TypeModel InitModelChilds(TypeModel model, Dictionary<string, TypeModel> types)
        {
            foreach (var typeName in model.ChildsTypeName)
            {
                if (types.Keys.Contains(typeName))
                {
                    model.Childs.Add(types[typeName]);
                }
                else
                {
                    model.Childs.Add(new TypeModel { Name = typeName });                    
                }
            }

            return model;
        }
    }
}
