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

        public Dictionary<string, TypeModel> TypesAsDictionary
        {
            get
            {
                var result = new Dictionary<string, TypeModel>();

                foreach (var type in Types)
                {
                    if (result.Keys.Contains(type.Name) == false)
                    {
                        result.Add(type.Name, type);
                    }
                }

                return result;
            }
        }

        public LibraryParser(string filePath)
        {
            _filePath = filePath;

            Assembly asm = Assembly.LoadFrom(_filePath);

            Types = asm.DefinedTypes.Select(a => new TypeModel(a)).ToList();
        }

        public IEnumerable<TypeModel> Parse()
        {
            var result = new List<TypeModel>();

            foreach (var item in Types)
            {
                var model = InitModelChilds(item);
                result.Add(model);
            }

            return result;
        }

        private TypeModel InitModelChilds(TypeModel model)
        {
            model.PropertiesChilds = GetChilds(model.Properties, TypesAsDictionary);
            model.FieldsChilds = GetChilds(model.Fields, TypesAsDictionary);

            return model;
        }

        private List<TypeModel> GetChilds(IEnumerable<TypeModel> fields, Dictionary<string, TypeModel> types)
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
