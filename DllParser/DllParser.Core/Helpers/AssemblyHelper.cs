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
        //public static TypeModel GetTypeModel(TypeInfo type)
        //{
        //    TypeModel model = new TypeModel 
        //    {
        //        Name = type.Name,
        //        Id = type.GUID
        //    };

        //    model.Methods = type.DeclaredMethods.Select(a => a.Name).ToList();
        //    model.Events = type.DeclaredEvents.Select(a => a.Name).ToList();
        //    model.Properties = type.DeclaredProperties.Select(a => a.PropertyType.Name).ToList();

        //    return model;
        //}

        public static TypeModel InitModelChilds(TypeModel model, Dictionary<string, TypeModel> types)
        {
            model.PropertiesChilds = GetChilds(model.Properties, types);
            model.FieldsChilds = GetChilds(model.Fields, types);

            return model;
        }

        private static List<TypeModel> GetChilds(IEnumerable<TypeModel> fields, Dictionary<string, TypeModel> types)
        {
            var result = new List<TypeModel>();

            foreach (var filed in fields)
            {
                if (types.Keys.Contains(filed.TypeName))
                {
                    result.Add(types[filed.TypeName]);
                }
                else
                {
                    result.Add(filed);                    
                }
            }

            return result;
        }
    }
}
