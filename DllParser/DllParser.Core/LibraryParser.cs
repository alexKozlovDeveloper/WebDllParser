using DllParser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DllParser.Core
{
    public class LibraryParser
    {
        private string _filePath;

        public List<TypeModel> Types { get; private set; }
        public Dictionary<string, List<TypeModel>> Namespaces { get; private set; }

        public List<string> NamespaceNames 
        { 
            get 
            {
                return Namespaces.Select(a => a.Key).ToList();
            } 
        }

        public LibraryParser(string filePath)
        {
            _filePath = filePath;

            Assembly asm = Assembly.LoadFrom(_filePath);

            Types = new List<TypeModel>();
            Namespaces = new Dictionary<string, List<TypeModel>>();

            foreach (var item in asm.DefinedTypes)
            {
                var model = new TypeModel(item);

                if (string.IsNullOrEmpty(model.Namespace) == true) { continue; }                

                if (Namespaces.Keys.Contains(model.Namespace) == false)
                {
                    Namespaces.Add(model.Namespace, new List<TypeModel>());
                }

                Namespaces[model.Namespace].Add(model);
                Types.Add(model);
            }
        }

        public TypeModel GetTypeModel(string typeName)
        {
            return Types.Where(a => a.Name == typeName).FirstOrDefault();
        }
    }
}
