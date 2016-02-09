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
            model.Childs = type.DeclaredFields.Select(a => a.Name).ToList();

            return model;
        }
    }
}
